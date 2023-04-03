using System.ComponentModel.DataAnnotations;

namespace MarketPlace.DAL.Entities;

public class CustomerProfile : Entity
{
    [Range(1, 100)]
    public int Age { get; set; }

    [StringLength(256, MinimumLength = 4)]
    [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)(\.(\w)+)$")]
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;

}
