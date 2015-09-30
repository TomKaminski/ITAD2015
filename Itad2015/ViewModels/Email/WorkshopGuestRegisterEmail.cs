namespace Itad2015.ViewModels.Email
{
    public class WorkshopGuestRegisterEmail: GuestRegisterEmail
    {
        public WorkshopGuestRegisterEmail(string to, string from, string title) : base(to, from, title)
        {
        }

        public string SchoolName { get; set; }
        public string WorkshopTitle { get; set; }
    }
}