using Nethereum.Hex.HexTypes;
using StreamJsonRpc;


namespace Web3CS.Protocol.EVM
{
    public interface IEVMProtocol: IOptimisticProtocol
    {
        /// <summary>
        /// Get Remote Version of Node
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#web3_clientversion
        /// </summary>
        /// <returns>Version of Node (e.g. Like HTTP UserAgent)</returns>
        [JsonRpcMethod("web3_clientVersion")]
        Task<string> GetWeb3VersionAsync();


        /// <summary>
        /// Returns Keccak-256 (not the standardized SHA3-256) of the given data.
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#web3_sha3
        /// </summary>
        /// <param name="data">Data To Convert To SHA3 Hash</param>
        /// <returns>SHA3 (Keccak) Encoded Result </returns>
        [JsonRpcMethod("web3_sha3")]
        Task<string> GenerateKeccakStringAsync(string data);

        /// <summary>
        /// Get Current Network ID (chainlist.org)
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#net_version
        /// </summary>
        /// <returns>Network Identifier</returns>
        [JsonRpcMethod("net_version")]
        Task<string> GetNetworkIDAsync();

        /// <summary>
        /// Is Endpoint Actively Listening For Connections
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#net_listening
        /// </summary>
        /// <returns>Boolean - true when listening, otherwise false.</returns>
        [JsonRpcMethod("net_listening")]
        Task<bool> GetEndpointListeningStateAsync();

        /// <summary>
        /// Get Endpoint Peer Count
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#net_listening
        /// </summary>
        /// <returns>Number of Connected Peers</returns>
        [JsonRpcMethod("net_peerCount")]
        Task<HexBigInteger> GetEndpointPeerCountAsync();


        /// <summary>
        /// Protocol Version Used By Endpoint
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#eth_protocolversion
        /// </summary>
        /// <returns>Running Version</returns>
        [JsonRpcMethod("eth_protocolVersion")]
        Task<string> GetEndpointProtocolVersionAsync();

        /// <summary>
        /// Endpoint Sync Status
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#eth_syncing
        /// </summary>
        /// <returns>SyncObject if Syncing, False if not.</returns>
        [JsonRpcMethod("eth_syncing")]
        Task<dynamic> GetEndpointSyncStatusAsync();
        /*this returns false for the metis endpoint which indicates its kind of working!.. need to test it on 
         * a node that is syncing.. tested and its still false.. however there is this issue - https://github.com/ethereum/go-ethereum/issues/25534
         * 
         * also then need to return a custom object.. something like SyncResults, which also includes the data for both objects
         * would need to then figure out how to present that via the SDK for both a user and proxy perspective 
         * (server would need to return both object types)
         */



        /// <summary>
        /// Endpoint Coinbase
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#eth_coinbase
        /// </summary>
        /// <returns>Coinbase Address</returns>
        [JsonRpcMethod("eth_coinbase")]
        Task<string> GetEndpointCoinbaseAsync();



        /// <summary>
        /// Is Endpoint Actively Mining
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#eth_mining
        /// </summary>
        /// <returns>Boolean - true when mining, otherwise false.</returns>
        [JsonRpcMethod("eth_mining")]
        Task<bool> GetEndpointMiningStateAsync();



        /// <summary>
        /// Is Endpoints Hashes Per Secons (hps)
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#eth_hashrate
        /// </summary>
        /// <returns>Hex Number in hashes</returns>
        [JsonRpcMethod("eth_hashrate")]
        Task<HexBigInteger> GetEndpointHashRateAsync();


        /// <summary>
        /// Network Gas Price
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#eth_gasprice
        /// </summary>
        /// <returns>Gas Price In WEI</returns>
        [JsonRpcMethod("eth_gasPrice")]
        Task<HexBigInteger> GetGasPriceAsync();

        /// <summary>
        /// Get Endpoint Accounts
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#eth_accounts
        /// </summary>
        /// <returns>Associated Accounts</returns>
        [JsonRpcMethod("eth_accounts")]
        Task<string[]> GetEndpointAccountsAsync();

        /// <summary>
        /// Get Latest Block
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#eth_blocknumber
        /// </summary>
        /// <returns>Block Number</returns>
        [JsonRpcMethod("eth_blockNumber")]
        Task<HexBigInteger> GetLatestBlockNumberAsync();


        /// <summary>
        /// Get Account Balance (At Block X)
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#eth_getbalance
        /// </summary>
        /// <returns>Account Balance (WEI)</returns>
        [JsonRpcMethod("eth_getBalance")]
        Task<HexBigInteger> GetBalanceAsync(string address, HexBigInteger blockNumber);


        /// <summary>
        /// Get Account Balance (pending/latest/earliest)
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#eth_getbalance
        /// </summary>
        /// <returns>Account Balance (WEI)</returns>
        [JsonRpcMethod("eth_getBalance")]
        Task<HexBigInteger> GetBalanceAsync(string address, string defaultBlockParamerer);




        /// <summary>
        /// Get Storage Slow (At Block X)
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#eth_getstorageat
        /// </summary>
        /// <returns>Value of Storage Slot</returns>
        [JsonRpcMethod("eth_getStorageAt")]
        Task<string> GetSlotAtAsync(string storageAddress, HexBigInteger storagePosition, HexBigInteger blockNumber);


        /// <summary>
        /// Get Storage Slow (At Block X)
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#eth_getstorageat
        /// </summary>
        /// <returns>Value of Storage Slot</returns>
        [JsonRpcMethod("eth_getStorageAt")]
        Task<byte[]> GetSlotAtAsync(string storageAddress, HexBigInteger storagePosition, string defaultBlockParamerer);

        /// <summary>
        /// No. TXs Sent From Address
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#eth_gettransactioncount
        /// </summary>
        /// <returns>TX Count</returns>
        [JsonRpcMethod("eth_getTransactionCount")]
        Task<HexBigInteger> GetTransactionCountAsync(string storageAddress, string defaultBlockParamerer);


        /// <summary>
        /// No. TXs In a Block (By Block Hash)
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#eth_gettransactioncount
        /// </summary>
        /// <returns>TX Count</returns>
        [JsonRpcMethod("eth_getBlockTransactionCountByHash")]
        Task<HexBigInteger> GetTransactionCountByHashAsync(string blockHash);


        /// <summary>
        /// No. TXs In a Block (By Block-Num/pending/latest/earliest/)
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#eth_getblocktransactioncountbynumber
        /// </summary>
        /// <returns>TX Count</returns>
        [JsonRpcMethod("eth_getBlockTransactionCountByNumber")]
        Task<HexBigInteger> GetTransactionCountByNumberAsync(string defaultBlockParamerer);


        /// <summary>
        /// No. Uncles In A block 
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#eth_getunclecountbyblockhash
        /// </summary>
        /// <returns>Number of Uncles In Block</returns>
        [JsonRpcMethod("eth_getUncleCountByBlockHash")]
        Task<HexBigInteger> GetUncleCountByBlockHashAsync(string blockHash);



        /// <summary>
        /// No. Uncles In A block 
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#eth_getunclecountbyblocknumber
        /// </summary>
        /// <returns>Number of Uncles In Block</returns>
        [JsonRpcMethod("eth_getUncleCountByBlockNumber")]
        Task<HexBigInteger> GetUncleCountByBlockNumberAsync(string blockHashorTag);


        /// <summary>
        /// Returns a Contract Code For The Given Address
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#eth_getcode
        /// </summary>
        /// <param name="address">Target Address</param>
        /// <param name="defaultBlockParamerer">pending/latest/earliest</param>
        /// <returns>Code</returns>
        [JsonRpcMethod("eth_getCode")]
        Task<string> GetCodeAsync(string address, string defaultBlockParamerer);


        /// <summary>
        /// Signs a Message Using Specified Address
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#eth_sign
        /// </summary>
        /// <param name="address">Target Address</param>
        /// <param name="message">Message To Sign</param>
        /// <returns>Signed Message</returns>
        [JsonRpcMethod("eth_sign")]
        Task<string> SignMessageAsync(string address, string message);


        /// <summary>
        /// Sign TX To Broadcast Later
        /// https://ethereum.org/en/developers/docs/apis/json-rpc/#eth_signtransaction
        /// </summary>
        /// <param name="txObject">TX Data</param>
        /// <returns></returns>
        [JsonRpcMethod("eth_signTransaction")]
        Task<string> SignTransactionAsync(string txObject);
    }
}
