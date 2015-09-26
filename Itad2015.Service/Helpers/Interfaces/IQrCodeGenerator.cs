using System.Drawing;

namespace Itad2015.Service.Helpers.Interfaces
{
    public interface IQrCodeGenerator
    {
        Bitmap GenerateQrCode(string data);
        string GenerateQrCodeStringSrc(Bitmap qr);
    }
}
