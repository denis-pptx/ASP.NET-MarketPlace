namespace MarketPlace.WEB.Areas.Admin.Models;

public class ProductListViewModel
{
    public IEnumerable<Product> Products { get; set; } = null!;
    public int ShopId { get; set; }
}
