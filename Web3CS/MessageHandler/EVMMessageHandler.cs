﻿using System.Buffers;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft;
using Microsoft.VisualStudio.Threading;
using Nerdbank.Streams;
using Newtonsoft.Json;
using StreamJsonRpc;
using StreamJsonRpc.Protocol;

namespace Web3CS.MessageHandler
{
    public class EVMMessageHandler : IJsonRpcMessageHandler
    {
        private static readonly ReadOnlyCollection<string> AllowedContentTypes = new ReadOnlyCollection<string>(new string[]
        {
        "application/json-rpc",
        "application/json",
        "application/jsonrequest",
        });

        /// <summary>
        /// The Content-Type header to use in requests.
        /// </summary>
        private static readonly MediaTypeHeaderValue ContentTypeHeader = new MediaTypeHeaderValue("application/json");

        /// <summary>
        /// The Accept header to use in requests.
        /// </summary>
        private static readonly MediaTypeWithQualityHeaderValue AcceptHeader = new MediaTypeWithQualityHeaderValue("application/json");

        private readonly HttpClient httpClient;
        private readonly Uri requestUri;
        private readonly AsyncQueue<HttpResponseMessage> incomingMessages = new AsyncQueue<HttpResponseMessage>();

        /// <summary>
        /// Backing field for the <see cref="TraceSource"/> property.
        /// </summary>
        private TraceSource traceSource = new TraceSource(nameof(JsonRpc));

        /// <summary>
        /// This is the path after the FQDN, (www.moo.com/test) Has a target of "/test"
        /// EVM JSON-RPC isnt quite compliant and everything is sent to the root "/" path
        /// </summary>
        private readonly string targetPath = "/";

        /// <summary>
        /// Initializes a new instance of the <see cref="EVMMessageHandler"/> class
        /// with the default <see cref="JsonMessageFormatter"/>.
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient"/> to use for transmitting JSON-RPC requests.</param>
        /// <param name="requestUri">The URI to POST to where the entity will be the JSON-RPC message.</param>
        public EVMMessageHandler(HttpClient httpClient, Uri requestUri)
            : this(httpClient, requestUri, new JsonMessageFormatter())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EVMMessageHandler"/> class.
        /// </summary>
        /// <param name="httpClient">The <see cref="HttpClient"/> to use for transmitting JSON-RPC requests.</param>
        /// <param name="requestUri">The URI to POST to where the entity will be the JSON-RPC message.</param>
        /// <param name="formatter">The message formatter.</param>
        public EVMMessageHandler(HttpClient httpClient, Uri requestUri, IJsonRpcMessageFormatter formatter)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.requestUri = requestUri ?? throw new ArgumentNullException(nameof(requestUri));
            this.Formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        }

        /// <summary>
        /// Event IDs raised to our <see cref="TraceSource"/>.
        /// </summary>
        public enum TraceEvent
        {
            /// <summary>
            /// An HTTP response with an error status code was received.
            /// </summary>
            HttpErrorStatusCodeReceived,
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Diagnostics.TraceSource"/> used to trace details about the HTTP transport operations.
        /// </summary>
        /// <value>The value can never be null.</value>
        /// <exception cref="ArgumentNullException">Thrown by the setter if a null value is provided.</exception>
        public TraceSource TraceSource
        {
            get => this.traceSource;
            set
            {
                Requires.NotNull(value, nameof(value));
                this.traceSource = value;
            }
        }

        /// <inheritdoc/>
        public bool CanRead => true;

        /// <inheritdoc/>
        public bool CanWrite => true;

        /// <inheritdoc/>
        public IJsonRpcMessageFormatter Formatter { get; }

        /// <inheritdoc/>
        public async ValueTask<JsonRpcMessage?> ReadAsync(CancellationToken cancellationToken)
        {
            var response = await this.incomingMessages.DequeueAsync(cancellationToken).ConfigureAwait(false);

            var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using(var sequence = new Sequence<byte>())
            {
                var buffer = ArrayPool<byte>.Shared.Rent(4096);
                try
                {
                    int bytesRead;
                    while(true)
                    {
                        bytesRead = await responseStream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
                        if(bytesRead == 0)
                        {
                            break;
                        }

                        var memory = sequence.GetMemory(bytesRead);
                        buffer.AsMemory(0, bytesRead).CopyTo(memory);
                        sequence.Advance(bytesRead);
                    }
                }
                finally
                {
                    ArrayPool<byte>.Shared.Return(buffer);
                }

                return this.Formatter.Deserialize(sequence);
            }
        }

        public async ValueTask WriteAsync(JsonRpcMessage content, CancellationToken cancellationToken)
        {
            // Cast here because we only support transmitting requests anyway.
            var contentAsRequest = (JsonRpcRequest)content;



            // new StringContent("{\"name\":\"John Doe\",\"age\":33}", Encoding.UTF8, "application/json");
            // The JSON-RPC over HTTP spec requires that we supply a Content-Length header, so we have to serialize up front
            // in order to measure its length.
            using(var sequence = new Sequence<byte>())
            {
                this.Formatter.Serialize(sequence, content);

                string requestBody = await new StreamReader(sequence.AsReadOnlySequence.AsStream()).ReadToEndAsync(); ;

                HttpResponseMessage response = await httpClient.PostAsync(targetPath, new StringContent(requestBody, Encoding.UTF8, "application/json"));
                if(response.IsSuccessStatusCode)
                {
                    VerifyThrowStatusCode(contentAsRequest.IsResponseExpected ? HttpStatusCode.OK : HttpStatusCode.NoContent, response.StatusCode);
                }
                else
                {
                    this.TraceSource.TraceEvent(TraceEventType.Error, (int)TraceEvent.HttpErrorStatusCodeReceived, "Received HTTP {0} {1} response to JSON-RPC request for method \"{2}\".", (int)response.StatusCode, response.StatusCode, contentAsRequest.Method);
                }

                // The response is expected to be a success code, or an error code with a content-type that we can deserialize.
                if(response.IsSuccessStatusCode || (response.Content?.Headers.ContentType?.MediaType is string mediaType && AllowedContentTypes.Contains(mediaType)))
                {
                    // Some requests don't merit response messages, such as notifications in JSON-RPC.
                    // Servers may communicate this with 202 or 204 HTTPS status codes in the response.
                    // Others may (poorly?) send a 200 response but with an empty entity.
                    if(response.Content?.Headers.ContentLength > 0)
                    {
                        // Make the response available for receiving.
                        this.incomingMessages.Enqueue(response);
                    }
                }
                else
                {
                    // Throw an exception because of the unexpected failure from the server without a JSON-RPC message attached.
                    response.EnsureSuccessStatusCode();
                }
            }


        }

        private static void VerifyThrowStatusCode(HttpStatusCode expected, HttpStatusCode actual)
        {
            if(expected != actual)
            {
                throw new BadRpcHeaderException($"Expected \"{(int)expected} {expected}\" response but received \"{(int)actual} {actual}\" instead.");
            }
        }


    }
}
