namespace Itad2015.Contract.Service
{
    public interface IPdfService
    {
        byte[] GeneratePdfFromView(string viewString, string[] cssPaths, string fontPath);
    }
}
