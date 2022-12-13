namespace Web3CS.Console
{
    public static class StringUtils
    {
        public static string Shorten(this string s)
        {
            return $"{s.Substring(0, 4)}...{s.Substring(s.Length -4)}";
        }
    }
}
