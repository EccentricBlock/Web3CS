using System.Runtime.CompilerServices;

namespace Web3CS.Protocol.EVM.RPCObjects
{



    //https://ethereum.org/en/developers/docs/apis/json-rpc/#eth_getblockbyhash
    //See return object
    //todo: add descriptions and smarten up
    public class EVMBlock<T> where T : class
    {
        public string difficulty { get; set; }
        public string extraData { get; set; }
        public string gasLimit { get; set; }
        public string gasUsed { get; set; }
        public string hash { get; set; }
        public string logsBloom { get; set; }
        public string miner { get; set; }
        public string mixHash { get; set; }
        public string nonce { get; set; }
        public string number { get; set; }
        public string parentHash { get; set; }
        public string receiptsRoot { get; set; }
        public string sha3Uncles { get; set; }
        public string size { get; set; }
        public string stateRoot { get; set; }
        public string timestamp { get; set; }
        public string totalDifficulty { get; set; }
        public T[] transactions { get; set; }
        public string transactionsRoot { get; set; }
        public string[] uncles { get; set; }
    }//public class Result


    public class EVMBlock : EVMBlock<EVMBlockTransaction>
    {

    }

    public static class EVMBlockExtensions
    {
        /// <summary>
        /// Converts The EVMBlock<String> object to a EVMBlock<EVMBlockTransaction> one
        /// </summary>
        public static EVMBlock<EVMBlockTransaction> ConvertToFullObject(this EVMBlock<string> blockData)
        {
            EVMBlock<EVMBlockTransaction> returnData = new EVMBlock<EVMBlockTransaction>
            {
                difficulty = blockData.difficulty,
                extraData = blockData.extraData,
                gasLimit = blockData.gasLimit,
                gasUsed = blockData.gasUsed,
                hash = blockData.hash,
                logsBloom = blockData.logsBloom,
                miner = blockData.miner,
                mixHash = blockData.mixHash,
                nonce = blockData.nonce,
                number = blockData.number,
                parentHash = blockData.parentHash,
                receiptsRoot = blockData.receiptsRoot,
                sha3Uncles = blockData.sha3Uncles,
                size = blockData.size,
                stateRoot = blockData.stateRoot,
                timestamp = blockData.timestamp,
                totalDifficulty = blockData.totalDifficulty,
                transactionsRoot = blockData.transactionsRoot,
                uncles = blockData.uncles,
                transactions = new EVMBlockTransaction[blockData.transactions.Length],
            };

            for(int i = 0; i < blockData.transactions.Length; i++)
            {
                returnData.transactions[i] = new EVMBlockTransaction
                                                            {
                                                                hash = blockData.transactions[i],
                                                                blockNumber = blockData.number,
                                                                blockHash = blockData.hash
                                                            };
            }//for(int i = 0; i < blockData.transactions.Length; i++)

            return returnData;
        }//public EVMBlock<EVMBlockTransaction> ConvertToFullObject(this EVMBlock<string> blockData)

        public static EVMBlock<EVMBlockTransaction>[] ConvertToFullObject(this EVMBlock<string>[] blockArrayData)
        {
            EVMBlock<EVMBlockTransaction>[] returnData = new EVMBlock<EVMBlockTransaction>[blockArrayData.Length];

            for(int i = 0; i < blockArrayData.Length; i++)
            {
                returnData[i] = blockArrayData[i].ConvertToFullObject();
            }//for(int i = 0; i <blockArrayData.Length; i++)

            return returnData;
        }//public static EVMBlock<EVMBlockTransaction>[] ConvertToFullObject(this EVMBlock<string>[] blockArrayData)
    }//public static class EVMBlockExtensions
}
