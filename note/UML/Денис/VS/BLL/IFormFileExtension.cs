using Microsoft.AspNetCore.Http;

namespace MarketPlace.BLL.Extensions;

public static class IFormFileExtension
{
    public static byte[] ToByteArray(this IFormFile formfile)
    {

        using (var memoryStream = new MemoryStream())
        {
            formfile.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
