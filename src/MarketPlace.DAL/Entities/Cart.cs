namespace MarketPlace.DAL.Entities;

public class Cart : Entity
{
    public virtual List<Product> Products { get; set; } = new();
    public int CustomerId { get; set; }
}
