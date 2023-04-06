using System.Net;

namespace MarketPlace.BLL.Infrastracture;

public class Response<T>
{
    public string Description { get; set; } = string.Empty;
    public HttpStatusCode StatusCode { get; set; }
    public T? Data { get; set;}
}
