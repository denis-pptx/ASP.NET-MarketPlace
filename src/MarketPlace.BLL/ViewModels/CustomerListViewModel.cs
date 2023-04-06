namespace MarketPlace.BLL.ViewModels;

public class CustomerListViewModel
{
    public IEnumerable<Customer> Customers { get; }
    public CustomerListViewModel(IEnumerable<Customer> customers)
    {
        Customers = customers.OrderBy(s => s.Login);
    }
}
