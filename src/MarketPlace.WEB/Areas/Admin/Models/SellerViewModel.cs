namespace MarketPlace.WEB.Models;

public class SellerViewModel
{
    public Seller Seller { get; set; } = null!;
    public SelectList Shops { get; set; } = null!;
}
