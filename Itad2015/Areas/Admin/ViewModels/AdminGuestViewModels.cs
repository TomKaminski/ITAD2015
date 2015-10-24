namespace Itad2015.Areas.Admin.ViewModels
{
    public class AdminGuestViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public string Size { get; set; }
        public bool IsCheckIn { get; set; }
        public bool ShirtOrdered { get; set; }
        public bool QrEmailSent { get; set; }
        public int? WorkshopGuestId { get; set; }
    }

    public class AdminWorkshopGuestViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public string Size { get; set; }
        public bool IsCheckIn { get; set; }
        public bool ShirtOrdered { get; set; }
        public bool QrEmailSent { get; set; }
        public int? WorkshopGuestId { get; set; }
    }
}