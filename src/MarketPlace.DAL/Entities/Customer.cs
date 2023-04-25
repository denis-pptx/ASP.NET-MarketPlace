namespace MarketPlace.DAL.Entities;

public class Customer : User
{
    public Profile? Profile { get; set; }
    public Cart? Cart { get; set; }
}
