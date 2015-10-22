namespace Itad2015.Areas.Admin.ViewModels.PdfViewModels
{
    public class QrTicketViewModel
    {
        public string QrSrc { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RegisterNumber { get; set; }

        public int MaxNumberForShirt { get; set; }
    }
}