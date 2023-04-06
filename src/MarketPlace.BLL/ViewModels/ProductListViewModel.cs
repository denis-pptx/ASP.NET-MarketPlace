namespace MarketPlace.BLL.ViewModels;

public class ProductListViewModel
{
    public IEnumerable<Product> Products { get; }

    public ProductListViewModel(IEnumerable<Product> products)
    {
        Products = products.OrderBy(s => s.Name);
    }
}
