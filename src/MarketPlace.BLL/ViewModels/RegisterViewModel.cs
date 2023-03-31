using System.ComponentModel.DataAnnotations;

namespace MarketPlace.BLL.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Не указан логин")]
    public string Login { get; set; } = null!;

    [Required(ErrorMessage = "Не указан пароль")]
    public string Password { get; set; } = null!;

    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string PasswordConfirm { get; set; } = null!;
}
