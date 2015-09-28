namespace Itad2015.Contract.Service
{
    public interface IImageProcessorService
    {
        void ProcessAndSaveImage(byte[] image, string path);

        void DeleteImagesByPath(string path);
    }
}
