using System.Collections.Generic;
using Itad2015.Contract.DTO.GetDto;
using Itad2015.Contract.DTO.PostDto;

namespace Itad2015.Contract.Service
{
    public interface IExcelService
    {
        void SaveFile(byte[] file, string path);
        void DeleteFile(string path);
        IEnumerable<ExcelFileItem> GetEmailData(ExcelPostFileDto fileOptions);
        byte[] GetShirtsFile(List<GuestShirtGetDto> notOrderedShirts);
    }
}
