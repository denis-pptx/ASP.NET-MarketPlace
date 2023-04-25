namespace MarketPlace.DAL.Entities;

public class Customer : User
{
    public Profile Profile { get; set; } = new();
    public Cart Cart { get; set; } = new();
}
