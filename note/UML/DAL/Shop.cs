namespace MarketPlace.DAL.Entities;

public class Shop : Entity
{
    [Display(Name = "Название")]
    [Required(ErrorMessage = "Не указано название")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Некорректное название")]
    public string Name { get; set; } = null!;

    [Display(Name = "Описание")]
    [StringLength(1000, MinimumLength = 0, ErrorMessage = "Некорректное описание")]
    public string? Description { get; set; }

    public virtual List<Product> Products { get; set; } = new();
}
