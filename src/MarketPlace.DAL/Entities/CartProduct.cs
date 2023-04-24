namespace MarketPlace.DAL.Entities;

public class CartProduct : Entity
{
    public int? ProductId { get; set; }
    public int? CartId { get; set; }
}
