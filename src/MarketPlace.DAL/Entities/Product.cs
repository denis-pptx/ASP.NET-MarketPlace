namespace MarketPlace.DAL.Entities;

public class Product : Entity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public double Price { get; set; }
    public ProductCategory Category { get; set; }

    public int ShopId { get; set; }
    public List<Cart> Carts { get; set; } = null!;
}
