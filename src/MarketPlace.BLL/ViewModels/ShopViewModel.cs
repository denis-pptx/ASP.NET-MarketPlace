using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.BLL.ViewModels;

public class ShopViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Не указано название")]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
