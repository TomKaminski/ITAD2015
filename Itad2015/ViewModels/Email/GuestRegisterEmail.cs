namespace Itad2015.ViewModels.Email
{
    public class GuestRegisterEmail:EmailCommon
    {
        public GuestRegisterEmail(string to, string from, string title) : base(to, from, title)
        {
        }

        public int Id { get; set; }
        public string ConfirmationHash { get; set; }
        public string CancelationHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}