using MarketPlace.DAL.Response;

namespace MarketPlace.WEB.ViewModels;

public class ErrorViewModel
{

    public int StatusCode { get; }
	public string Description { get; }

	public ErrorViewModel(HttpStatusCode httpStatusCode, string description)
	{
		StatusCode = (int)httpStatusCode;
		Description = description;
	}

    public ErrorViewModel((HttpStatusCode httpStatusCode, string description) tuple) 
        : this(tuple.httpStatusCode, tuple.description)
    {

    }
}
