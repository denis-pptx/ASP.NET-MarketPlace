namespace MarketPlace.WEB.Areas.Admin.Models;

public class ShopListViewModel
{
    public IEnumerable<Shop> Shops { get; set; } = null!;
    public string? Name { get; set; }
}
