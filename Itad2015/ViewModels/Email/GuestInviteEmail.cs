namespace Itad2015.ViewModels.Email
{
    public class GuestInviteEmail:EmailCommon
    {
        public GuestInviteEmail(string to, string @from, string title) : base(to, @from, title)
        {
        }
        public string Bcc { get; set; }
    }

    public class GuestQrEmail : EmailCommon
    {
        public GuestQrEmail(string to, string @from, string title) : base(to, @from, title)
        {
        }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}