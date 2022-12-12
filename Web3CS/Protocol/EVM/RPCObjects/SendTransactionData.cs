namespace Web3CS.Protocol.EVM.RPCObjects
{
    //todo: add summaries and tidy-up code
    //https://ethereum.org/en/developers/docs/apis/json-rpc/#eth_signtransaction
    public class SendTransactionData
    {
        public string data { get; set; }
        public string from { get; set; }
        public string gas { get; set; }
        public string gasPrice { get; set; }
        public string to { get; set; }
        public string value { get; set; }
        public string nonce { get; set; }
    }
}

