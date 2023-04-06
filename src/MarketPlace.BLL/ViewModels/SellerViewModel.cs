namespace MarketPlace.BLL.ViewModels;

public class SellerViewModel
{
    public Seller Seller { get; set; }
    public SelectList Shops { get; } = null!;
    public SellerViewModel(IEnumerable<Shop> shops, Seller? seller = null)
    {
        Seller = seller != null ? seller : new Seller();
        Shops = new SelectList(shops.OrderBy(s => s.Name), "Id", "Name", Seller.ShopId);
    }
}
