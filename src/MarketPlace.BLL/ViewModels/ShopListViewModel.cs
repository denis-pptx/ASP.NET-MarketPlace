namespace MarketPlace.BLL.ViewModels;

public class ShopListViewModel
{
    public IEnumerable<Shop> Shops { get; }
    public string? Name { get; set; }

    public ShopListViewModel(IEnumerable<Shop> shops)
    {
        Shops = shops.OrderBy(s => s.Name);
    }
}
