namespace MarketPlace.WEB.ViewModels;

public class ShopListViewModel
{
    public IEnumerable<Shop> Shops { get; set; } = null!;
    public string? Name { get; set; }
}
