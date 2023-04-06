namespace MarketPlace.DAL.Entities;

public class Seller : User
{
    public int ShopId { get; set; }
    public virtual Shop? Shop { get; set; } = null!;
}
