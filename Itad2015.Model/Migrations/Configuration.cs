using System;
using Itad2015.Model.Concrete;

namespace Itad2015.Model.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ItadDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ItadDbContext context)
        {
            context.Guest.Add(new Guest
            {
                Email = "tkaminski93@gmail.com",
                FirstName = "Tomasz",
                LastName = "Kamiñski",
                RegistrationTime = DateTime.Now
            });

            context.SaveChanges();
        }
    }
}
