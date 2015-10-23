using System.Data.Entity;
using Itad2015.Model;

namespace Itad2015.Modules.Infrastructure
{
    public static class InitializerModule
    {
        public static void InitializeDb()
        {
            Database.SetInitializer(new ItadDbContext.ItadDbInitializer());
        }

        public static void DeleteUsers()
        {
            new ItadDbContext().DeleteUsers();
        }
    }
}
