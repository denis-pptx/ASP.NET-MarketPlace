namespace MarketPlace.DAL.Entities;

public class Cart : Entity
{
    public virtual List<CartItem> Items { get; set; } = new();
    public int CustomerId { get; set; }
}
