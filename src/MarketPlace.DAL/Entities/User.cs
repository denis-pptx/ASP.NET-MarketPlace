using MarketPlace.DAL.Enum;

namespace MarketPlace.DAL.Entities;

public class User : Entity
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Role Role { get; set; }
}
