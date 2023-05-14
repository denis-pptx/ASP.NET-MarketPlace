namespace MarketPlace.DAL.Entities;

public class Product : Entity
{
    [Display(Name = "Название")]
    public string Name { get; set; } = null!;

    [Display(Name = "Описание")]
    public string? Description { get; set; }

    [Display(Name = "Цена")]
    public double Price { get; set; }

    [Display(Name = "Категория")]
    public ProductCategory Category { get; set; }

    public byte[]? Photo { get; set; }

    public int ShopId { get; set; }
}
