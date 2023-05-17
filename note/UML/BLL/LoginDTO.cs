namespace MarketPlace.BLL.DTO;

public class LoginDTO
{
    [Display(Name = "Логин")]
    [Required(ErrorMessage = "Не указан логин")]
    [RegularExpression(@"^[a-zA-Z_]{2,50}$", ErrorMessage = "Некорректный логин")]
    public string Login { get; set; } = null!;

    [Display(Name = "Пароль")]
    [Required(ErrorMessage = "Не указан пароль")]
    [RegularExpression(@"^[a-zA-Z_]{2,50}$", ErrorMessage = "Некорректный пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
