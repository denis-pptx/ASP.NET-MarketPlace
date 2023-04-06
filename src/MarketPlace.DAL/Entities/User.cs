namespace MarketPlace.DAL.Entities;

public class User : Entity
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;

    [NotMapped]
    [Compare("Password")]
    public string? PasswordConfirm { get; set; } = null!;
    public Role Role { get; set; }
}
