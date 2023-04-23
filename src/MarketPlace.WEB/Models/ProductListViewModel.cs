namespace MarketPlace.WEB.Models;

public class ProductListViewModel
{
    public IEnumerable<Product> Products { get; set; } = null!;
    public SelectList AllCategories { get; set; } = null!;
    public SelectList SelectedCategories { get; set; } = null!;
    public string SearchString { get; set; } = null!;
}
