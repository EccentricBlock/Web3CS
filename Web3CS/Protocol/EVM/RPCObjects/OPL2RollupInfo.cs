namespace Web3CS.Protocol.EVM.RPCObjects
{
    //https://community.optimism.io/docs/developers/build/json-rpc/#rollup-getinfo
    public class OPL2RollupInfo
    {
        public string mode { get; set; }
        public bool syncing { get; set; }
        public Ethcontext ethContext { get; set; }
        public Rollupcontext rollupContext { get; set; }
    }//public class OPL2RollupInfo

    public class Ethcontext
    {
        public int blockNumber { get; set; }
        public int timestamp { get; set; }
    }//public class Ethcontext

    public class Rollupcontext
    {
        public int index { get; set; }
        public int queueIndex { get; set; }
        public int verifiedIndex { get; set; }
    }//public class Rollupcontext
}


