namespace MarketPlace.DAL.Entities;

public class Customer : User
{
    public CustomerProfile? Profile { get; set; } = null!;
    public Cart? Cart { get; set; } = null!;
}
