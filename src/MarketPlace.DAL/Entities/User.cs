namespace MarketPlace.DAL.Entities;

public class User : Entity
{
    [Display(Name = "Логин")]
    [Required(ErrorMessage = "Не указан логин")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Некорректный логин")]
    public string Login { get; set; } = null!;

    [Display(Name = "Пароль")]
    [Required(ErrorMessage = "Не указан пароль")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Некорректный телефон")]
    public string Password { get; set; } = null!;
    public Role Role { get; set; } = Role.Customer;
}
