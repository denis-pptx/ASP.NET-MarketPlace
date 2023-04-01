namespace MarketPlace.BLL.Infrastracture;

public enum StatusCode
{
    UserNotFound,
    WrongPassword,
    LoginIsUsed,

    ShopNameIsUsed,
    ShopNotFound,


    OK = 200,
    InternalServerError = 500
}
