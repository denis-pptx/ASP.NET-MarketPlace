namespace MarketPlace.DAL.Entities;

public class OrderItem : Entity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public ProductCategory Category { get; set; }
    public byte[]? Photo { get; set; }


    public int? ProductId { get; set; }
    public int OrderId { get; set; }
}
