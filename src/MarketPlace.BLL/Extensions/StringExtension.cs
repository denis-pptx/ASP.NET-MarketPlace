namespace MarketPlace.BLL.Extensions;

public static class StringExtension
{
    public static string RemoveWhitespaces(this string str)
    {
        return Regex.Replace(str, @"\s+", "");
    }
}
