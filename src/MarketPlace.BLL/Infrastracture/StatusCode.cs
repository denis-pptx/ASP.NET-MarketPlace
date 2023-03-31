namespace MarketPlace.BLL.Infrastracture;

public enum StatusCode
{
    UserNotFound,
    WrongPassword,
    LoginIsUsed,

    OK = 200,
    InternalServerError = 500
}
