namespace MarketPlace.DAL.Response;

public class Response<T>
{
    public HttpStatusCode StatusCode { get; set; }
    public string Description { get; set; } = string.Empty;
    public T? Data { get; set;}

    public (HttpStatusCode, string) Deconstruct()
    {
        return (StatusCode, Description);
    }
}
