namespace MarketPlace.DAL.Entities;

public class Profile : Entity
{
    [Display(Name = "Возраст")]
    [Required(ErrorMessage = "Не указан возраст")]
    [Range(1, 100, ErrorMessage = "Некорректный возраст")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Не указана почта")]
    [StringLength(256, MinimumLength = 4, ErrorMessage = "Некорректный адрес электронной почты")]
    [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)(\.(\w)+)$", ErrorMessage = "Некорректный адрес электронной почты")]
    public string Email { get; set; } = null!;

    [Display(Name = "Телефон")]
    [Phone(ErrorMessage = "Некорректный телефон")]
    [Required(ErrorMessage = "Не указан телефон")]
    public string Phone { get; set; } = null!;

    public int CustomerId { get; set; }
}
