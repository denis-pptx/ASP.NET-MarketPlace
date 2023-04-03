namespace MarketPlace.DAL.Entities;

public class Customer : User
{
    public int ProfileId { get; set; }
    public virtual CustomerProfile Profile { get; set; } = null!;
}
