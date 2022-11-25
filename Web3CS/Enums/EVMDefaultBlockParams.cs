namespace Web3CS.Enums
{
    public enum EVMDefaultBlockParams
    {
        Latest,
        Pending,
        Earliest,
    }//public enum EVMDefaultBlockParams

    public static class EVMDefaultBlockParamsStatic
    {
        public static string ToRPCString(this EVMDefaultBlockParams enumValue)
        {
            return enumValue switch
            {
                EVMDefaultBlockParams.Latest => "latest",
                EVMDefaultBlockParams.Pending => "pending",
                EVMDefaultBlockParams.Earliest => "earliest",
                _ => "0x0",//hex string
            };
        }//public static string ToRPCString(this EVMDefaultBlockParams enumValue)
    }//public static class EVMDefaultBlockParamsStatic
}