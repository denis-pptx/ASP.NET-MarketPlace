# MarketPlace

### Admin Area
Доступ к этой области имеют только авторизованные пользователи с ролью Admin.

- **Shop Controller**
CRUD для магазинов
    - `/Admin/Shop/Index` - получить все магазины
    - `/Admin/Shop/Index/{name}` - получить все магазины с похожим именем
    - `/Admin/Shop/Create` - получить форму для создания магазина
    - `/Admin/Shop/Create/{shop}` - создать магазин
    - `/Admin/Shop/Edit/{id}` - получить форму для изменения магазина
    - `/Admin/Shop/Edit/{shop}` - изменить магазин
    - `/Admin/Shop/Delete/{id}` - удалить магазин


- **Seller Controller**
CRUD для продавцов
    - `/Admin/Seller/Index[/{shopId}]` - получить продавцов, относящихся к магазину
    - `/Admin/Seller/Create` - получить форму для создания продавца
    - `/Admin/Seller/Create/{seller}` - создать продавца
    - `/Admin/Seller/Edit/{id}` - получить форму для изменения продавца
    - `/Admin/Seller/Edit/{seller}` - изменить продавца
    - `/Admin/Seller/Delete/{id}` - удалить продавца


- **Seller Controller**
CRUD для покупателей
    - `/Admin/Customer/Index` - получить всех покупателей
    - `/Admin/Customer/Create` - получить форму для создания покупателя
    - `/Admin/Customer/Create/{customer}` - создать покупателя
    - `/Admin/Customer/Edit/{id}` - получить форму для изменения покупателя
    - `/Admin/Customer/Edit/{customer}` - изменить покупателя
    - `/Admin/Customer/Delete/{id}` - удалить покупателя

### Seller Area
Доступ к этой области имеют только авторизованные пользователи с ролью Seller.

- **Product Controller**
CRUD для товаров
    - `/Seller/Product/Index` - получить все товары из магазина текущего продавца
    - `/Seller/Product/Create` - получить форму для создания товара
    - `/Seller/Product/Create/{product}` - создать товар
    - `/Seller/Product/Edit/{id}` - получить форму для изменения товара
    - `/Seller/Product/Edit/{product}` - изменить товар
    - `/Seller/Product/Delete/{id}` - удалить товар


- **Shop Controller**
    - `/Seller/Shop/Index` - получить магазин текущего продавца

### Outside Areas
Контроллеры вне области. Доступ имеют все.

- **Home Controller**
    - `/Home/Index` - главная страница


- **Account Controller**
    - `/Account/Login` - форма для авторизации
    - `/Account/Login/{loginViewModel}` - авторизация
    - `/Account/Register` - форма для регистрации
    - `/Account/Register/{customer}` - регистрация
    - `/Account/Logout` - выход
    - `/Account/AccessDenied` - страница `StatusCode(403, "Forbidden")`


## Примечание
 - При обращении неаутентифицированного пользователя к ресурсам из областей, требующих авторизацию, произойдет перенаправление на страницу `/Account/Login`.
 - При обращении аутентифицированного пользователя к ресурсу, доступ к которому он не емеет, произойдет перенаправление на страницу `/Account/AccessDenied`.
