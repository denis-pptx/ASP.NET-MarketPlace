namespace MarketPlace.DAL.Entities;

public class Customer : User
{
    public virtual CustomerProfile Profile { get; set; } = null!;
}
