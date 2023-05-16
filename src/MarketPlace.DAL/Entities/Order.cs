namespace MarketPlace.DAL.Entities;

public class Order : Entity
{
    public int? CustomerId { get; set; }
    public int? ShopId { get; set; }

    public virtual List<OrderItem> Items { get; set; } = new();
}
