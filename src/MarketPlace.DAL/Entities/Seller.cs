namespace MarketPlace.DAL.Entities;

public class Seller : User
{
    public int ShopId { get; set; }
    public Shop? Shop { get; set; }
}
