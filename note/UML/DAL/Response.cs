namespace MarketPlace.DAL.Response;

public class Response<T> : BaseResponse
{
    public T Data { get; set; } = default!;
}
