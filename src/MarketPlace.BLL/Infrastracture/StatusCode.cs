namespace MarketPlace.BLL.Infrastracture;

public enum StatusCode
{
    UserNotFound,
    WrongPassword,
    LoginIsUsed,

    ShopNameIsUsed,
    ShopNotFound,

    UserLoginIsUsed,
    SellerNotFound,

    OK = 200,
    InternalServerError = 500
}
