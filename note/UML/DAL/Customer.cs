namespace MarketPlace.DAL.Entities;

public class Customer : User
{
    public virtual Profile? Profile { get; set; }
    public virtual Cart? Cart { get; set; }
}
