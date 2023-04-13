namespace MarketPlace.WEB.ViewModels;

public class SellerViewModel
{
    public Seller? Seller { get; set; }
    public SelectList Shops { get; set; } = null!;
}
