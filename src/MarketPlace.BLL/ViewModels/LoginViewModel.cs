namespace MarketPlace.BLL.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Не указан логин")]
    public string Login { get; set; } = null!;

    [Required(ErrorMessage = "Не указан пароль")]
    public string Password { get; set; } = null!;
}
