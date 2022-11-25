using Microsoft;
using System.Diagnostics;

namespace Web3CS.Utils
{
    public class Traceable
    {
        /// <summary>
        /// Event IDs raised to our <see cref="TraceSource"/>.
        /// </summary>
        public enum TraceEvent
        {
            /// <summary>
            /// An HTTP response with an error status code was received.
            /// </summary>
            GeneralException,
        }


        /// <summary>
        /// Backing field for the <see cref="TraceSource"/> property.
        /// </summary>
        internal TraceSource traceSource = new TraceSource(nameof(Traceable));


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
    }
}

