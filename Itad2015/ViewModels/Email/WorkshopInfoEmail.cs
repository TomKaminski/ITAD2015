using System.EnterpriseServices.Internal;

namespace Itad2015.ViewModels.Email
{
    public class WorkshopInfoEmail : EmailCommon
    {
        public WorkshopInfoEmail(string to, string @from, string title) : base(to, @from, title)
        {
        }

        public string Name { get; set; }
        public string WorkshopName { get; set; }
        public string WorkshopHour { get; set; }
        public string WorkshopRoom { get; set; }
        public string TutorName { get; set; }
    }


}