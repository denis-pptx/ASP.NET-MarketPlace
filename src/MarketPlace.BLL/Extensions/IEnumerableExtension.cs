using MarketPlace.DAL.Response;

namespace MarketPlace.BLL.Extensions;

public static class IEnumerableExtension
{
    public static SelectList GetCategories(this IEnumerable<Product> products)
    {
        var categories = products.Select(c => c.Category).Distinct();
        return new(categories.Select(c => new { Id = (int)c, Name = c.GetDisplayName() })
                             .OrderBy(a => a.Name), "Id", "Name");
    }

    public static IEnumerable<Product> Sort(this IEnumerable<Product> products, SortOrder order)
    {
        return order switch
        {
            SortOrder.Name => products.OrderBy(p => p.Name),
            SortOrder.NameDescending => products.OrderByDescending(p => p.Name),
            SortOrder.Price => products.OrderBy(p => p.Price),
            SortOrder.PriceDescending => products.OrderByDescending(p => p.Price),
            SortOrder.Category => products.OrderBy(p => p.Category),
            SortOrder.CategoryDescending => products.OrderByDescending(p => p.Category),
            _ => products
        };
    }
}
