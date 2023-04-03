namespace MarketPlace.BLL.Infrastracture;

public enum StatusCode
{
    UserNotFound,
    WrongPassword,
    LoginIsUsed,

    ShopNameIsUsed,
    ShopNotFound,

    SellerNotFound,

    CustomerNotFound,

    OK = 200,
    InternalServerError = 500
}
