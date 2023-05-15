namespace MarketPlace.BLL.DTO;

public class LoginDTO
{
    [Display(Name = "Логин")]
    [Required(ErrorMessage = "Не указан логин")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Некорректный логин")]
    public string Login { get; set; } = null!;

    [Display(Name = "Пароль")]
    [Required(ErrorMessage = "Не указан пароль")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Некорректный телефон")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
