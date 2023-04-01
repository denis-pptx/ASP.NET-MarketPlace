using MarketPlace.DAL.Entities;
using MarketPlace.DAL.Enum;

namespace MarketPlace.BLL.ViewModels;

public class SellerViewModel
{
    public int Id { get; set; }
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int ShopId { get; set; }
    public ShopViewModel Shop { get; set; } = null!;
}
