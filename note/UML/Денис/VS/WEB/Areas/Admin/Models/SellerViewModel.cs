namespace MarketPlace.WEB.Areas.Admin.Models;

public class SellerViewModel
{
    public DAL.Entities.Seller Seller { get; set; } = null!;
    public SelectList Shops { get; set; } = null!;
}
