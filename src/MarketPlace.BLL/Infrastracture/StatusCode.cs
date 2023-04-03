﻿namespace MarketPlace.BLL.Infrastracture;

public enum StatusCode
{
    UserNotFound,
    WrongPassword,
    LoginIsUsed,

    ShopNameIsUsed,
    ShopNotFound,

    SellerNotFound,

    CustomerNotFound,

    ProductNotFound,

    OK = 200,
    InternalServerError = 500
}
