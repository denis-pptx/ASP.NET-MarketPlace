namespace MarketPlace.BLL.DTO;

public class RegisterDTO
{
    [Display(Name = "Логин")]
    [Required(ErrorMessage = "Не указан логин")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Некорректный логин")]
    public string Login { get; set; } = null!;


    [Display(Name = "Пароль")]
    [Required(ErrorMessage = "Не указан пароль")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Некорректный пароль")]
    public string Password { get; set; } = null!;

    [Display(Name = "Подтверждение пароля")]
    [Required(ErrorMessage = "Повторите пароль")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string PasswordConfirm { get; set; } = null!;

    [Display(Name = "Возраст")]
    [Required(ErrorMessage = "Не указан возраст")]
    [Range(1, 100, ErrorMessage = "Некорректный возраст")]
    public int Age { get; set; }

    [Display(Name = "Почта")]
    [Required(ErrorMessage = "Не указана почта")]
    [StringLength(256, MinimumLength = 4, ErrorMessage = "Некорректный адрес электронной почты")]
    [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)(\.(\w)+)$", ErrorMessage = "Некорректный адрес электронной почты")]
    public string Email { get; set; } = null!;

    [Display(Name = "Телефон")]
    [Phone(ErrorMessage = "Некорректный телефон")]
    [Required(ErrorMessage = "Не указан телефон")]
    public string Phone { get; set; } = null!;
}
