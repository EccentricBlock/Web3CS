namespace Web3CS.Utils
{
    public static class StringUtils
    {
        public static string Shorten(this string s)
        {
            return $"{s.Substring(0, 4)}...{s.Substring(s.Length - 4)}";
        }//public static string Shorten(this string s)
    }//public static class StringUtils
}
