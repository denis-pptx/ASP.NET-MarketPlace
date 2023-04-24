namespace MarketPlace.DAL.Entities;

public class Cart : Entity
{
    public List<Product> Products { get; set; } = new();
    public int CustomerId { get; set; }
}
