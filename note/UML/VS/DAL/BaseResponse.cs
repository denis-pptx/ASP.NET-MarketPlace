using System.Net;

namespace MarketPlace.DAL.Response;

public abstract class BaseResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public string Description { get; set; } = string.Empty;

    public (HttpStatusCode, string) Deconstruct()
    {
        return (StatusCode, Description);
    }
}
