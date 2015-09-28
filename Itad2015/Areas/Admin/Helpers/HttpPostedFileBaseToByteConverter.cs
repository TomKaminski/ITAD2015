using System.IO;

namespace Itad2015.Areas.Admin.Helpers
{
    public static class HttpPostedFileBaseToByteConverter
    {
        public static byte[] Convert(Stream file)
        {
            var convertedFile = new byte[file.Length];
            file.Read(convertedFile, 0, (int)file.Length);
            return convertedFile;
        }
    }
}