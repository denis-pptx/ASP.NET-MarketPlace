namespace MarketPlace.DAL.Entities;

public class Product : Entity
{
    [Display(Name = "Название")]
    [Required(ErrorMessage = "Не указано название")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Некорректное название")]
    public string Name { get; set; } = null!;

    [Display(Name = "Описание")]
    public string? Description { get; set; }

    [Display(Name = "Цена")]
    public double Price { get; set; }

    [Display(Name = "Количество")]
    [Range(0, 9999, ErrorMessage = "Некорректное количество")]
    public int Quantity { get; set; }

    [Display(Name = "Категория")]
    public ProductCategory Category { get; set; }

    public byte[]? Photo { get; set; }

    public virtual Shop? Shop { get; set; }
    public int ShopId { get; set; }
}
