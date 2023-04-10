namespace MarketPlace.DAL.Entities;

public class Customer : User
{
    public CustomerProfile Profile { get; set; } = null!;
}
