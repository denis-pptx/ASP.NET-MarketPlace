namespace MarketPlace.BLL.ViewModels;

public class SellerListViewModel
{
    public IEnumerable<Seller> Sellers { get; }
    public SelectList Shops { get; }

    public SellerListViewModel(IEnumerable<Seller> sellers, IEnumerable<Shop> shops, int? selectedValue)
    {
        Sellers = sellers.OrderBy(s => s.Login);

        var orderedShops = shops.OrderBy(s => s.Name).ToList();
        orderedShops.Insert(0, new Shop() { Id = 0, Name = "Все" });
        Shops = new SelectList(orderedShops, "Id", "Name", selectedValue);
    }
}
