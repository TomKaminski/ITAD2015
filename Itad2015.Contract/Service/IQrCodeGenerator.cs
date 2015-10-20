using System.Drawing;

namespace Itad2015.Contract.Service
{
    public interface IQrCodeGenerator
    {
        Bitmap GenerateQrCode(string data);
        string GenerateQrCodeStringSrc(byte[] qr);
        byte[] GenerateQrAsByteArray(Bitmap qr);
    }
}
