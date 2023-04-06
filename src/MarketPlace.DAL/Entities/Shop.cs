namespace MarketPlace.DAL.Entities;

public class Shop : Entity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public virtual List<Product>? Products { get; set; } = null!;
}
