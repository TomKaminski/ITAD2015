namespace Itad2015.ViewModels.Email
{
    public abstract class EmailCommon : Postal.Email
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Title { get; set; }

        protected EmailCommon(string to, string from, string title)
        {
            To = to;
            Title = title;
            From = from;
        }
    }
}