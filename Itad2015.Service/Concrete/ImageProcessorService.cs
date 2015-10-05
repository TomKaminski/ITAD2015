using System.IO;
using ImageResizer;
using Itad2015.Contract.Service;

namespace Itad2015.Service.Concrete
{
    public class ImageProcessorService : IImageProcessorService
    {
        public void ProcessAndSaveImage(byte[] image, string pathWithName)
        {
            var imageBig = new ImageJob(image, pathWithName + "_normal", new Instructions("maxwidth=1500&maxheight=800&format=jpg"))
            {
                CreateParentDirectory = true,
                AddFileExtension = true
            };
            imageBig.Build();
            var imageSmall = new ImageJob(image, pathWithName + "_small", new Instructions("maxwidth=500&maxheight=300&format=jpg"))
            {
                CreateParentDirectory = true,
                AddFileExtension = true
            };
            imageSmall.Build();
        }

        public void DeleteImagesByPath(string path)
        {
            if (File.Exists($"{path}_normal.jpg"))
                File.Delete($"{path}_normal.jpg");
            if (File.Exists($"{path}_small.jpg"))
                File.Delete($"{path}_small.jpg");
        }
    }
}
