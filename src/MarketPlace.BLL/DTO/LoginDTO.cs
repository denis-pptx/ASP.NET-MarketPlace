namespace MarketPlace.BLL.DTO;

public class LoginDTO
{
    [Required(ErrorMessage = "Не указан логин")]
    public string Login { get; set; } = null!;

    [Required(ErrorMessage = "Не указан пароль")]
    public string Password { get; set; } = null!;
}
