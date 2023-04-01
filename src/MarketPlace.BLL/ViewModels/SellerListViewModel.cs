using MarketPlace.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MarketPlace.BLL.ViewModels;

public class SellerListViewModel
{
    public IEnumerable<SellerViewModel> Sellers { get; set; } = new List<SellerViewModel>();
    public SelectList Shops { get; set; } = new SelectList(new List<Shop>(), "Id", "Name");
}
