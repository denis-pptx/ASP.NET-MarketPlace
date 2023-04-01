using MarketPlace.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MarketPlace.BLL.ViewModels;

public class SellerListViewModel
{
    public IEnumerable<Seller> Sellers { get; }
    public SelectList Shops { get; }

    public SellerListViewModel(IEnumerable<Seller> sellers, IEnumerable<Shop> shops, int? selectedValue)
    {
        Sellers = sellers.OrderBy(s => s.Login);

        var sortedShops = shops.Skip(1).OrderBy(s => s.Name).ToList();
        sortedShops.Insert(0, shops.First());
        Shops = new SelectList(sortedShops, "Id", "Name", selectedValue);
    }
}
