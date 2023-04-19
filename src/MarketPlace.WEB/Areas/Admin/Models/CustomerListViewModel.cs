namespace MarketPlace.WEB.Areas.Admin.Models;

public class CustomerListViewModel
{
    public IEnumerable<DAL.Entities.Customer> Customers { get; set; } = null!;
}
