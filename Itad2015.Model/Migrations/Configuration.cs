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
            context.Guest.AddOrUpdate(new Guest
            {
                Email = "tkaminski93@gmail.com",
                FirstName = "Tomasz",
                LastName = "Kamiñski",
                RegistrationTime = DateTime.Now
            });

            var saltHash = PasswordHasherHelper.CreateHash("password123");
            char[] delimiter = { ':' };
            var split = saltHash.Split(delimiter);
            var salt = split[0];
            var hash = split[1];
            context.User.AddOrUpdate(new User
            {
                Email = "tkaminski93@gmail.com",
                SuperAdmin = true,
                PasswordHash = hash,
                PasswordSalt = salt
            });
            context.SaveChanges();
        }
    }
}
