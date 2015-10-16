using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Excel;
using Itad2015.Contract.DTO.GetDto;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Contract.Service;
using Itad2015.Contract.Service.Entity;
using OfficeOpenXml;

namespace Itad2015.Service.Concrete
{
    public class ExcelService : IExcelService
    {
        private readonly IGuestService _guestService;

        public ExcelService(IGuestService guestService)
        {
            _guestService = guestService;
        }

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

            var list = new HashSet<ExcelFileItem>();

            for (var i = fileOptions.HasHeader ? 1 : 0; i < worksheet.Rows.Length; i++)
            {
                if (worksheet.Rows[i].Cells[0] != null)
                {
                    var item = new ExcelFileItem
                    {
                        Email = worksheet.Rows[i].Cells[fileOptions.EmailPosition - 1].Text,
                        Name = worksheet.Rows[i].Cells[fileOptions.NamePosition - 1].Text,
                        LastName = worksheet.Rows[i].Cells[fileOptions.LastNamePosition - 1].Text
                    };
                    if(list.FirstOrDefault(x => x.Email == item.Email) == null)
                        list.Add(item);

                }
            }
            return list;
        }

        public byte[] GetShirtsFile(List<GuestShirtGetDto> notOrderedShirts)
        {
            using (var pck = new ExcelPackage())
            {
                //Create the worksheet
                var ws = pck.Workbook.Worksheets.Add($"NotOrderedShirts-{DateTime.Today.ToShortDateString()}");

                var properties = notOrderedShirts.First().GetType().GetProperties();

                for (var i = 0; i < properties.Length; i++)
                {
                    ws.Cells[1, i + 1].Value = properties[i].Name;
                }

                ws.Row(1).Style.Font.Bold = true;

                for (var j = 0; j < notOrderedShirts.Count; j++)
                {
                    var item = notOrderedShirts[j];
                    for (var i = 0; i < properties.Length; i++)
                    {
                        ws.Cells[j+2, i+1].Value = item.GetType().GetProperty(properties[i].Name).GetValue(item);
                    }
                }

                return pck.GetAsByteArray();
            }
        }
    }
}
