namespace MarketPlace.DAL.Entities;

public class User : Entity
{
    [Display(Name = "Логин")]
    [Required(ErrorMessage = "Не указан логин")]
    [RegularExpression(@"^[a-zA-Z_]{2,50}$", ErrorMessage = "Некорректный логин")]
    public string Login { get; set; } = null!;

    [Display(Name = "Пароль")]
    [Required(ErrorMessage = "Не указан пароль")]
    [RegularExpression(@"^[a-zA-Z_]{2,50}$", ErrorMessage = "Некорректный пароль")]
    public string Password { get; set; } = null!;
    public Role Role { get; set; } = Role.Customer;
}
