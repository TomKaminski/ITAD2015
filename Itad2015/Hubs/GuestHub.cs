using Microsoft.AspNet.SignalR;

namespace Itad2015.Hubs
{
    public class GuestHub:Hub
    {
        public void NotifyCheck(int id, bool checkedIn)
        {
            // Call the notifyCheck method to update clients.
            Clients.All.notifyCheck(new
            {
                id,
                checkedIn
            });
        }
    }
}