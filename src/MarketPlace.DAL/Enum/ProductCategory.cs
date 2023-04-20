namespace MarketPlace.DAL.Enum;

public enum ProductCategory
{
    [Display(Name = "Недвижимость")]
    Realty = 1,

    [Display(Name = "Авто и транспорт")]
    Transport,

    [Display(Name = "Бытовая техника")]
    HouseholdEquipment,

    [Display(Name = "Компьютерная техника")]
    ComputerEquipment,

    [Display(Name = "Телефоны и планшеты")]
    Phones,

    [Display(Name = "Электроника")]
    Electronics,

    [Display(Name = "Гардероб")]
    Wardrobe,

    [Display(Name = "Красота и здоровье")]
    Beauty,

    [Display(Name = "Всё для детей и мам")]
    Children,

    [Display(Name = "Мебель")]
    Furniture,

    [Display(Name = "Всё для дома")]
    Household,

    [Display(Name = "Ремонт и стройка")]
    Renovation,

    [Display(Name = "Сад и огрод")]
    Garden,

    [Display(Name = "Хобби, спорт и туризм")]
    Sport,

    [Display(Name = "Животные")]
    Animals,

    [Display(Name = "Услуги")]
    Services
}
