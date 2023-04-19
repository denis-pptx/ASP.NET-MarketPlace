namespace MarketPlace.WEB.Areas.Admin.Models;

public class SellerListViewModel
{
    public IEnumerable<DAL.Entities.Seller> Sellers { get; set; } = null!;
    public SelectList Shops { get; set; } = null!;
    public int ShopId { get; set; }
}
