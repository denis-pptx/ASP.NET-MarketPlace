namespace MarketPlace.DAL.Entities;

public class User : Entity
{
    [Required(ErrorMessage = "Не указан логин")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Некорректный логин")]
    public string Login { get; set; } = null!;

    [Required(ErrorMessage = "Не указан пароль")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Некорректный телефон")]
    public string Password { get; set; } = null!;
    public Role Role { get; set; } = Role.Customer;
}
