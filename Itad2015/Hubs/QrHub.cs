using Microsoft.AspNet.SignalR;

namespace Itad2015.Hubs
{
    public class ExcelHub:Hub
    {

        public void NotifyEmailSent(string userId, string email)
        {
            Clients.User(userId).notifyEmailSent(email);
        }
    }
}