using MarketPlace.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MarketPlace.BLL.ViewModels;

public class SellerListViewModel
{
    public IEnumerable<Seller> Sellers { get; set; } = new List<Seller>();
    public SelectList Shops { get; set; } = new SelectList(new List<Shop>(), "Id", "Name");
}
