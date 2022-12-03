using Nethereum.Hex.HexTypes;
using Newtonsoft.Json.Linq;
using StreamJsonRpc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using Web3CS.Enums;
using Web3CS.MessageHandler;
using Web3CS.Protocol.EVM;
using Web3CS.Protocol.EVM.RPCObjects;
using Web3CS.Utils;

namespace Web3CS
{
    public class Web3 : Traceable, IDisposable
    {


        private readonly HttpClient rpcHTTPClient;
        IJsonRpcMessageFormatter jsonRpcMessageFormatter = new JsonMessageFormatter(Encoding.UTF8);
        IJsonRpcMessageHandler jsonRpcMessageHandler;
        IEVMProtocol evmProtocol;

        public Web3(Uri rpcEndpoint)
        {
            traceSource = new TraceSource(nameof(Web3));

            rpcHTTPClient = new HttpClient()
            {
                BaseAddress = rpcEndpoint,
            };
            rpcHTTPClient.DefaultRequestHeaders.Accept.Clear();
            rpcHTTPClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("Eccentric_Web3CS", "0.1")));

            jsonRpcMessageHandler = new EVMMessageHandler(rpcHTTPClient, rpcEndpoint, jsonRpcMessageFormatter);

            evmProtocol = JsonRpc.Attach<IEVMProtocol>(jsonRpcMessageHandler);
        }//public Web3(Uri rpcEndpoint)


        public async Task<string> GetWeb3VersionAsync()
        {
            string returnValue = string.Empty;

            try
            {

                returnValue = await evmProtocol.GetWeb3VersionAsync().ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                this.TraceSource.TraceEvent(TraceEventType.Error, (int)Traceable.TraceEvent.GeneralException, ex.ToString());
                //todo: error Handling
                Console.WriteLine(ex.ToString());
            }

            return returnValue;
        }//public async Task<string> GetWeb3VersionAsync()

        public async Task<string> GenerateKeccakStringAsync(string data)
        {
            string returnValue = string.Empty;

            try
            {
                //IEVMProtocol evmProtocol = JsonRpc.Attach<IEVMProtocol>(jsonRpcMessageHandler);
                return await evmProtocol.GenerateKeccakStringAsync(data).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                this.TraceSource.TraceEvent(TraceEventType.Error, (int)Traceable.TraceEvent.GeneralException, ex.ToString());
                //todo: error Handling
                Console.WriteLine(ex.ToString());
            }

            return returnValue;
        }

        public async Task<string> GetNetworkIDAsync()
        {
            string returnValue = string.Empty;

            try
            {
                //IEVMProtocol evmProtocol = JsonRpc.Attach<IEVMProtocol>(jsonRpcMessageHandler);


                return await evmProtocol.GetNetworkIDAsync().ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                this.TraceSource.TraceEvent(TraceEventType.Error, (int)Traceable.TraceEvent.GeneralException, ex.ToString());
                //todo: error Handling
                Console.WriteLine(ex.ToString());
            }

            return returnValue;
        }

        public async Task<bool> GetEndpointListeningStateAsync()
        {
            bool returnValue = false;

            try
            {
                //IEVMProtocol evmProtocol = JsonRpc.Attach<IEVMProtocol>(jsonRpcMessageHandler);
                return await evmProtocol.GetEndpointListeningStateAsync().ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                this.TraceSource.TraceEvent(TraceEventType.Error, (int)Traceable.TraceEvent.GeneralException, ex.ToString());
                //todo: error Handling
                Console.WriteLine(ex.ToString());
            }

            return returnValue;
        }

        public async Task<HexBigInteger> GetEndpointPeerCountAsync()
        {
            HexBigInteger returnValue = new HexBigInteger(0);

            try
            {
                //IEVMProtocol evmProtocol = JsonRpc.Attach<IEVMProtocol>(jsonRpcMessageHandler);
                return await evmProtocol.GetEndpointPeerCountAsync().ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                this.TraceSource.TraceEvent(TraceEventType.Error, (int)Traceable.TraceEvent.GeneralException, ex.ToString());
                //todo: error Handling
                Console.WriteLine(ex.ToString());
            }

            return returnValue;
        }

        public async Task<string> GetEndpointProtocolVersionAsync()
        {
            string returnValue = string.Empty;

            try
            {
                //IEVMProtocol evmProtocol = JsonRpc.Attach<IEVMProtocol>(jsonRpcMessageHandler);
                return await evmProtocol.GetEndpointProtocolVersionAsync().ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                this.TraceSource.TraceEvent(TraceEventType.Error, (int)Traceable.TraceEvent.GeneralException, ex.ToString());
                //todo: error Handling
                Console.WriteLine(ex.ToString());
            }

            return returnValue;
        }


        public async Task<dynamic> GetEndpointSyncStatusAsync()
        {
            dynamic returnValue = string.Empty;

            try
            {
                //IEVMProtocol evmProtocol = JsonRpc.Attach<IEVMProtocol>(jsonRpcMessageHandler);
                return await evmProtocol.GetEndpointSyncStatusAsync().ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                this.TraceSource.TraceEvent(TraceEventType.Error, (int)Traceable.TraceEvent.GeneralException, ex.ToString());
                //todo: error Handling
                Console.WriteLine(ex.ToString());
            }

            return returnValue;
        }

        public async Task<string> GetEndpointCoinbaseAsync()
        {
            string returnValue = string.Empty;

            try
            {
                //IEVMProtocol evmProtocol = JsonRpc.Attach<IEVMProtocol>(jsonRpcMessageHandler);
                return await evmProtocol.GetEndpointCoinbaseAsync().ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                this.TraceSource.TraceEvent(TraceEventType.Error, (int)Traceable.TraceEvent.GeneralException, ex.ToString());
                //todo: error Handling
                Console.WriteLine(ex.ToString());
            }

            return returnValue;
        }

        public async Task<bool> GetEndpointMiningStateAsync()
        {
            bool returnValue = false;

            try
            {
                //IEVMProtocol evmProtocol = JsonRpc.Attach<IEVMProtocol>(jsonRpcMessageHandler);
                return await evmProtocol.GetEndpointMiningStateAsync().ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                this.TraceSource.TraceEvent(TraceEventType.Error, (int)Traceable.TraceEvent.GeneralException, ex.ToString());
                //todo: error Handling
                Console.WriteLine(ex.ToString());
            }

            return returnValue;
        }

        public async Task<HexBigInteger> GetEndpointHashRateAsync()
        {
            return await evmProtocol.GetEndpointHashRateAsync().ConfigureAwait(false);
        }

        public async Task<HexBigInteger> GetGasPriceAsync()
        {
            return await evmProtocol.GetGasPriceAsync().ConfigureAwait(false);
        }

        public async Task<string[]> GetEndpointAccountsAsync()
        {
            return await evmProtocol.GetEndpointAccountsAsync().ConfigureAwait(false);
        }

        public async Task<HexBigInteger> GetLatestBlockNumberAsync()
        {
            return await evmProtocol.GetLatestBlockNumberAsync().ConfigureAwait(false);
        }

        public async Task<HexBigInteger> GetBalanceAsync(string address, HexBigInteger blockNumber)
        {
            return await evmProtocol.GetBalanceAsync(address, blockNumber.HexValue).ConfigureAwait(false);
        }

        public async Task<HexBigInteger> GetBalanceAsync(string address, EVMDefaultBlockParams defaultBlockParamerer)
        {
            return await evmProtocol.GetBalanceAsync(address, defaultBlockParamerer.ToRPCString()).ConfigureAwait(false);
        }

        public async Task<string> GetSlotAtAsync(string address, HexBigInteger storagePosition, HexBigInteger blockNumber)
        {
            return await evmProtocol.GetSlotAtAsync(address, storagePosition, blockNumber).ConfigureAwait(false);
        }

        public async Task<byte[]> GetSlotAtAsync(string address, HexBigInteger storagePosition, EVMDefaultBlockParams defaultBlockParamerer)
        {
            return await evmProtocol.GetSlotAtAsync(address, storagePosition, defaultBlockParamerer.ToRPCString()).ConfigureAwait(false);
        }


        public async Task<HexBigInteger> GetTransactionCountAsync(string address, HexBigInteger blockNumber)
        {
            return await evmProtocol.GetTransactionCountAsync(address, blockNumber.HexValue).ConfigureAwait(false);
        }

        public async Task<HexBigInteger> GetTransactionCountAsync(string address, EVMDefaultBlockParams defaultBlockParamerer)
        {
            return await evmProtocol.GetTransactionCountAsync(address, defaultBlockParamerer.ToRPCString()).ConfigureAwait(false);
        }



        public async Task<HexBigInteger> GetTransactionCountByHashAsync(string blockHash)
        {
            return await evmProtocol.GetTransactionCountByHashAsync(blockHash).ConfigureAwait(false);
        }

        public async Task<HexBigInteger> GetTransactionCountByNumberAsync(EVMDefaultBlockParams defaultBlockParamerer)
        {
            return await evmProtocol.GetTransactionCountByNumberAsync(defaultBlockParamerer.ToRPCString()).ConfigureAwait(false);
        }

        public async Task<HexBigInteger> GetTransactionCountByNumberAsync(HexBigInteger blockNumber)
        {
            return await evmProtocol.GetTransactionCountByNumberAsync(blockNumber.HexValue).ConfigureAwait(false);
        }

        public async Task<HexBigInteger> GetUncleCountByBlockHashAsync(string blockHash)
        {
            return await evmProtocol.GetUncleCountByBlockHashAsync(blockHash).ConfigureAwait(false);
        }


        public async Task<HexBigInteger> GetUncleCountByBlockNumberAsync(EVMDefaultBlockParams defaultBlockParamerer)
        {
            return await evmProtocol.GetUncleCountByBlockNumberAsync(defaultBlockParamerer.ToRPCString()).ConfigureAwait(false);
        }

        public async Task<HexBigInteger> GetUncleCountByBlockNumberAsync(HexBigInteger blockNumber)
        {
            return await evmProtocol.GetUncleCountByBlockNumberAsync(blockNumber.HexValue).ConfigureAwait(false);
        }

        public async Task<string> GetCodeAsync(string address, EVMDefaultBlockParams defaultBlockParamerer)
        {
            return await evmProtocol.GetCodeAsync(address, defaultBlockParamerer.ToRPCString()).ConfigureAwait(false);
        }

        //https://bytemeta.vip/repo/ethereum-optimism/optimism/issues/1218
        public async Task<string> SignMessageAsync(string address, string messageToSign)
        {
            return await evmProtocol.SignMessageAsync(address, $"0x{Convert.ToHexString(Encoding.UTF8.GetBytes(messageToSign))}").ConfigureAwait(false);
        }


        public async Task<EVMBlock[]> L2_GetBlockRangeAsync(EVMDefaultBlockParams startingBlockTag, EVMDefaultBlockParams lastBlockTag, bool returnHashOnly = true)
        {
            return await evmProtocol.L2_GetBlockRangeAsync(startingBlockTag.ToRPCString(), lastBlockTag.ToRPCString(), returnHashOnly).ConfigureAwait(false);
        }

        public async Task<EVMBlock[]> L2_GetBlockRangeAsync(HexBigInteger startingBlockNumber, EVMDefaultBlockParams lastBlockTag, bool returnHashOnly = true)
        {
            return await evmProtocol.L2_GetBlockRangeAsync(startingBlockNumber.HexValue, lastBlockTag.ToRPCString(), returnHashOnly).ConfigureAwait(false);
        }

        public async Task<EVMBlock[]> L2_GetBlockRangeAsync(HexBigInteger startingBlockNumber, HexBigInteger lastBlockNumber, bool returnHashOnly = true)
        {
            return await evmProtocol.L2_GetBlockRangeAsync(startingBlockNumber.HexValue, lastBlockNumber.HexValue, returnHashOnly).ConfigureAwait(false);
        }

        public async Task<EVMBlock[]> L2_GetBlockRangeAsync(EVMDefaultBlockParams startingBlockTag, HexBigInteger lastBlockNumber, bool returnHashOnly = true)
        {
            return await evmProtocol.L2_GetBlockRangeAsync(startingBlockTag.ToRPCString(), lastBlockNumber.HexValue, returnHashOnly).ConfigureAwait(false);
        }


        public async Task<OPL2RollupInfo> L2_GetRollupInfoAsync()
        {
            return await evmProtocol.L2_GetRollupInfoAsync().ConfigureAwait(false);
        }

        public async Task<OPL2RollupGASPrices> L2_GASPricesAsync()
        {
            return await evmProtocol.L2_GASPricesAsync().ConfigureAwait(false);
        }
        

        public void Dispose()
        {

        }
    }//public class Web3 {
}
