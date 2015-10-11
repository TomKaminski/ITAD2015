using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Excel;
using Itad2015.Contract.DTO.GetDto;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Contract.Service;

namespace Itad2015.Service.Concrete
{
    public class ExcelService : IExcelService
    {
        public void SaveFile(byte[] file, string path)
        {
            DeleteFile(path);
            File.WriteAllBytes(path, file);
        }

        public void DeleteFile(string path)
        {
            if (File.Exists($"{path}"))
                File.Delete($"{path}");
        }

        public IEnumerable<ExcelFileItem> GetEmailData(ExcelPostFileDto fileOptions)
        {
            var worksheet = Workbook.Worksheets(fileOptions.FilePath).ToArray()[fileOptions.WorkSheetNumber - 1];

            var list = new List<ExcelFileItem>();

            for (var i = fileOptions.HasHeader ? 1 : 0; i < worksheet.Rows.Length; i++)
            {
                if (worksheet.Rows[i].Cells[0] != null)
                {
                    list.Add(new ExcelFileItem
                    {
                        Email = worksheet.Rows[i].Cells[fileOptions.EmailPosition - 1].Text,
                        Name = worksheet.Rows[i].Cells[fileOptions.NamePosition - 1].Text,
                        LastName = worksheet.Rows[i].Cells[fileOptions.LastNamePosition - 1].Text
                    });
                }
            }
            return list;
        }
    }
}
