namespace MarketPlace.BLL.Infrastracture;

internal class Response<T>
{
    public string Description { get; set; } = string.Empty;
    public StatusCode StatusCode { get; set; }
    public T? Data { get; set; }
}
