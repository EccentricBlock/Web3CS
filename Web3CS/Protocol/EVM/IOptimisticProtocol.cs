using StreamJsonRpc;
using Web3CS.Protocol.EVM.RPCObjects;

namespace Web3CS.Protocol.EVM
{
    //https://community.optimism.io/docs/developers/build/json-rpc/#
    public interface IOptimisticProtocol
    {
        /// <summary>
        /// Same As "eth_getBlockByNumber" (TX list is hashes)
        /// https://community.optimism.io/docs/developers/build/json-rpc/#eth-getblockrange
        /// </summary>
        /// <param name="startingBlockOrTag">BlockNum or Tag (pending/latest/earliest)</param>
        /// <param name="FinishingBlockOrTag">BlockNum or Tag (pending/latest/earliest)</param>
        /// <param name="returnHashOnly">false returns full blocks like "eth_getBlockByHash"</param>
        /// <returns>Array of Blocks (see eth_getblockbyhash)</returns>
        [JsonRpcMethod("eth_getBlockRange")]
        Task<EVMBlock<string>[]> L2_GetBlockRangeHashesAsync(string startingBlockOrTag, string FinishingBlockOrTag, bool returnHashOnly = true);

        /// <summary>
        /// Same As "eth_getBlockByNumber" (TX list is full objects)
        /// https://community.optimism.io/docs/developers/build/json-rpc/#eth-getblockrange
        /// </summary>
        /// <param name="startingBlockOrTag">BlockNum or Tag (pending/latest/earliest)</param>
        /// <param name="FinishingBlockOrTag">BlockNum or Tag (pending/latest/earliest)</param>
        /// <param name="returnHashOnly">false returns full blocks like "eth_getBlockByHash"</param>
        /// <returns>Array of Blocks (see eth_getblockbyhash)</returns>
        [JsonRpcMethod("eth_getBlockRange")]
        Task<EVMBlock[]> L2_GetBlockRangeFullTXObjectAsync(string startingBlockOrTag, string FinishingBlockOrTag, bool returnHashOnly = false);

        /// <summary>
        /// Same As "eth_getBlockByNumber" (TX list is hashes only)
        /// https://community.optimism.io/docs/developers/build/json-rpc/#eth-getblockrange
        /// </summary>
        /// <param name="startingBlockOrTag">BlockNum or Tag (pending/latest/earliest)</param>
        /// <param name="FinishingBlockOrTag">BlockNum or Tag (pending/latest/earliest)</param>
        /// <param name="returnHashOnly">false returns full blocks like "eth_getBlockByHash"</param>
        /// <returns>Array of Blocks (see eth_getblockbyhash)</returns>
        [JsonRpcMethod("eth_getBlockRange")]
        Task<EVMBlock<string>[]> L2_GetBlockRangeHashTXObjectAsync(string startingBlockOrTag, string FinishingBlockOrTag, bool returnHashOnly = false);



        /// <summary>
        /// Get L2-specific Info About Target Node
        /// https://community.optimism.io/docs/developers/build/json-rpc/#rollup-getinfo
        /// </summary>
        /// <returns></returns>
        [JsonRpcMethod("rollup_getInfo")]
        Task<OPL2RollupInfo> L2_GetRollupInfoAsync();


        /// <summary>
        /// Returns L1 and L2 GAS Prices Being by the Sequencer.
        /// https://community.optimism.io/docs/developers/build/json-rpc/#rollup-gasprices
        /// </summary>
        /// <returns></returns>
        [JsonRpcMethod("rollup_gasPrices")]
        Task<OPL2RollupGASPrices> L2_GASPricesAsync();
    }//public interface IOptimisticProtocol
}
