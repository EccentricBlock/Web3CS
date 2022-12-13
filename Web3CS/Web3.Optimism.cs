using Nethereum.Hex.HexTypes;
using Web3CS.Enums;
using Web3CS.Protocol.EVM.RPCObjects;

namespace Web3CS
{
    public partial class Web3
    {

        public async Task<OPL2RollupInfo> L2_GetRollupInfoAsync()
        {
            return await evmProtocol.L2_GetRollupInfoAsync().ConfigureAwait(false);
        }

        public async Task<OPL2RollupGASPrices> L2_GASPricesAsync()
        {
            return await evmProtocol.L2_GASPricesAsync().ConfigureAwait(false);
        }

        public async Task<EVMBlock<EVMBlockTransaction>[]> L2_GetBlockRangeAsync(EVMDefaultBlockParams startingBlockTag, EVMDefaultBlockParams lastBlockTag, bool txHashList = true)
        {
            if(!txHashList) { return await evmProtocol.L2_GetBlockRangeFullTXObjectAsync(startingBlockTag.ToRPCString(), lastBlockTag.ToRPCString(), !txHashList).ConfigureAwait(false);  }

            EVMBlock<string>[] initialData = await evmProtocol.L2_GetBlockRangeHashTXObjectAsync(startingBlockTag.ToRPCString(), lastBlockTag.ToRPCString(), txHashList).ConfigureAwait(false);
            
            return initialData.ConvertToFullObject();
        }//public async Task<EVMBlock<EVMBlockTransaction>[]> L2_GetBlockRangeAsync(EVMDefaultBlockParams startingBlockTag, EVMDefaultBlockParams lastBlockTag, bool fullTXObject = false)
        

        public async Task<EVMBlock<EVMBlockTransaction>[]> L2_GetBlockRangeAsync(HexBigInteger startingBlockNumber, EVMDefaultBlockParams lastBlockTag, bool txHashList = true)
        {
            if(!txHashList) { return await evmProtocol.L2_GetBlockRangeFullTXObjectAsync(startingBlockNumber.HexValue, lastBlockTag.ToRPCString(), !txHashList).ConfigureAwait(false); }

            EVMBlock<string>[] initialData = await evmProtocol.L2_GetBlockRangeHashTXObjectAsync(startingBlockNumber.HexValue, lastBlockTag.ToRPCString(), txHashList).ConfigureAwait(false);

            return initialData.ConvertToFullObject();
        }

        public async Task<EVMBlock<EVMBlockTransaction>[]> L2_GetBlockRangeAsync(HexBigInteger startingBlockNumber, HexBigInteger lastBlockNumber, bool txHashList = true)
        {
            if(!txHashList) { return await evmProtocol.L2_GetBlockRangeFullTXObjectAsync(startingBlockNumber.HexValue, lastBlockNumber.HexValue, !txHashList).ConfigureAwait(false); }

            EVMBlock<string>[] initialData = await evmProtocol.L2_GetBlockRangeHashTXObjectAsync(startingBlockNumber.HexValue, lastBlockNumber.HexValue, txHashList).ConfigureAwait(false);

            return initialData.ConvertToFullObject();
        }

        public async Task<EVMBlock<EVMBlockTransaction>[]> L2_GetBlockRangeWithFullTXObjectAsync(EVMDefaultBlockParams startingBlockTag, HexBigInteger lastBlockNumber, bool txHashList = true)
        {
            if(!txHashList) { return await evmProtocol.L2_GetBlockRangeFullTXObjectAsync(startingBlockTag.ToRPCString(), lastBlockNumber.HexValue, !txHashList).ConfigureAwait(false); }

            EVMBlock<string>[] initialData = await evmProtocol.L2_GetBlockRangeHashTXObjectAsync(startingBlockTag.ToRPCString(), lastBlockNumber.HexValue, txHashList).ConfigureAwait(false);

            return initialData.ConvertToFullObject();
        }

    }
}
