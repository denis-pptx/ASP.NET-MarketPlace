namespace MarketPlace.BLL.Helpers;

public static class StringHelper
{
    public static string RemoveWhitespaces(this string str)
    {
        return Regex.Replace(str, @"\s+", "");
    }
}
