namespace MarketPlace.DAL.Entities;

public class Cart : Entity
{
    public List<Product> Products { get; set; } = null!;
    public int CustomerId { get; set; }
}
