namespace MarketPlace.WEB.Areas.Seller.Models;

public class ProductListViewModel
{
    public IEnumerable<Product> Products { get; set; } = null!;
    public SelectList Categories { get; set; } = null!;
    public int CategoryId { get; set; }
}
