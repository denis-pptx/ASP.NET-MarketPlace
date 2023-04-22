namespace MarketPlace.BLL.DTO;

public class RegisterDTO
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;

    [Compare("Password")]
    public string PasswordConfirm { get; set; } = null!;
    public int Age { get; set; }
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
}
