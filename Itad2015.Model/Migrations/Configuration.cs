using Itad2015.Model.Concrete;

namespace Itad2015.Model.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Itad2015.Model.ItadDbContext>
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

            var saltHash = PasswordHasherHelper.CreateHash("xcwdwpkbwk");
            char[] delimiter = { ':' };
            var split = saltHash.Split(delimiter);
            var salt = split[0];
            var hash = split[1];


            var saltHash1 = PasswordHasherHelper.CreateHash("j5cdmwg6tpm1");
            char[] delimiter1 = { ':' };
            var split1 = saltHash1.Split(delimiter1);
            var salt1 = split1[0];
            var hash1 = split1[1];

            context.User.AddOrUpdate(new User
            {
                Email = "tkaminski93@gmail.com",
                SuperAdmin = true,
                PasswordHash = salt1,
                PasswordSalt = hash1
            });
            context.User.AddOrUpdate(new User
            {
                Email = "katarzynajasiewicz.93@gmail.com",
                SuperAdmin = true,
                PasswordHash = hash,
                PasswordSalt = salt
            });
            context.SaveChanges();
        }
    }
}
