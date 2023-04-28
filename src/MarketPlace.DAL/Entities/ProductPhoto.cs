namespace MarketPlace.DAL.Entities;

public class ProductPhoto : Entity
{
    public int ProductId { get; set; }
    public byte[] Photo { get; set; } = null!;
}
