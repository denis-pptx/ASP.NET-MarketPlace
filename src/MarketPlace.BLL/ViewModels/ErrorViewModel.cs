namespace MarketPlace.BLL.ViewModels;

public class ErrorViewModel
{
    public int StatusCode { get; }
	public string Description { get; }
	public ErrorViewModel(HttpStatusCode httpStatusCode, string description)
	{
		StatusCode = (int)httpStatusCode;
		Description = description;
	}
}
