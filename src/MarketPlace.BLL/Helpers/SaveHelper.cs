namespace MarketPlace.BLL.Helpers;

public class SaveHelper
{
    public static bool IsEdit(Entity? item)
    {
        if (item == null)
        {
            return false;
        }

        return item.Id != 0;
    }
}
