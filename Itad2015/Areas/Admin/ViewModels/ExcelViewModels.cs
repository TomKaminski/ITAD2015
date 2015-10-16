using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Itad2015.Areas.Admin.ViewModels
{
    public class ExcelPostViewModel
    {
        [Required]
        [Display(Name = "Nr arkusza")]
        public int WorkSheetNumber { get; set; }
        [Required]
        [Display(Name = "Nr kolumny z emailem")]
        public int EmailPosition { get; set; }
        [Required]
        [Display(Name = "Nr kolumny z imieniem")]
        public int NamePosition { get; set; }
        [Required]
        [Display(Name = "Nr kolumny z nazwiskiem")]
        public int LastNamePosition { get; set; }
        [Display(Name = "Posiada nagłówek")]
        public bool HasHeader { get; set; }
        [DataType(DataType.Upload)]
        [Required]
        public HttpPostedFileBase File { get; set; }
    }

    public class ExcelGetDataViewModel
    {

        public int WorkSheetNumber { get; set; }

        public int EmailPosition { get; set; }

        public int NamePosition { get; set; }

        public int LastNamePosition { get; set; }
        public string FileName { get; set; }
        public bool HasHeader { get; set; }
        public string ConnectionId { get; set; }
    }



    public class ExcelListItemViewModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool EmailSent { get; set; }
    }
}