namespace MarketPlace.BLL.Extensions;

public static class StringExtension
{
    public static string RemoveWhitespaces(this string str)
    {
        return Regex.Replace(str, @"\s+", "");
    }

    public static bool IsSimilar(this string a, string b)
    {
        // Приведем строки к нижнему регистру и удалим пробелы
        a = a.ToLower().Replace(" ", "");
        b = b.ToLower().Replace(" ", "");

        // Рассчитаем расстояние Левенштейна
        int[,] distances = new int[a.Length + 1, b.Length + 1];
        for (int i = 0; i <= a.Length; i++)
        {
            distances[i, 0] = i;
        }
        for (int j = 0; j <= b.Length; j++)
        {
            distances[0, j] = j;
        }
        for (int i = 1; i <= a.Length; i++)
        {
            for (int j = 1; j <= b.Length; j++)
            {
                int cost = (a[i - 1] == b[j - 1]) ? 0 : 1;
                distances[i, j] = Math.Min(Math.Min(
                    distances[i - 1, j] + 1,
                    distances[i, j - 1] + 1),
                    distances[i - 1, j - 1] + cost);
            }
        }

        // Проверим, является ли расстояние Левенштейна меньше определенного порога (например, 3)
        int maxDistance = 3;
        return distances[a.Length, b.Length] <= maxDistance;
    }
}
