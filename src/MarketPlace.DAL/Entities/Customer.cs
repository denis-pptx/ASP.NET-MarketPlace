namespace MarketPlace.DAL.Entities;

public class Customer : User
{
    public Profile Profile { get; set; } = null!;
    public Cart Cart { get; set; } = null!;
}
