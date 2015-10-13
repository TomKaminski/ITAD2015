using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Cryptography;
using Itad2015.Model.Concrete;

namespace Itad2015.Model
{
    public class ItadDbContext : DbContext
    {
        public ItadDbContext() : base("ItadDbContext")
        {
            Database.SetInitializer(new ItadDbInitializer());
        }

        public virtual IDbSet<Guest> Guest { get; set; }

        public virtual IDbSet<WorkshopGuest> WorkshopGuest { get; set; }

        public virtual IDbSet<Prize> Prize { get; set; }

        public virtual IDbSet<Workshop> Workshop { get; set; }

        public virtual IDbSet<User> User { get; set; } 


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Workshop>()
                .HasMany(x => x.WorkshopGuests)
                .WithRequired(x => x.Workshop)
                .HasForeignKey(x => x.WorkshopId);

            modelBuilder.Entity<WorkshopGuest>().HasRequired(x => x.Guest).WithMany().HasForeignKey(x => x.GuestId);

            base.OnModelCreating(modelBuilder);
        }

        public class ItadDbInitializer : CreateDatabaseIfNotExists<ItadDbContext>
        {
            protected override void Seed(ItadDbContext context)
            {
                context.Guest.Add(new Guest
                {
                    Email = "tkaminski93@gmail.com",
                    FirstName = "Tomasz",
                    LastName = "Kamiński",
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

    public static class PasswordHasherHelper
    {
        private const int SaltByteSize = 24;
        private const int HashByteSize = 24;
        private const int Pbkdf2Iterations = 1000;

        public static string CreateHash(string password)
        {
            // Generate a random salt
            var csprng = new RNGCryptoServiceProvider();
            var salt = new byte[SaltByteSize];
            csprng.GetBytes(salt);

            // Hash the password and encode the parameters
            var hash = Pbkdf2(password, salt, Pbkdf2Iterations, HashByteSize);
            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
        }

        private static byte[] Pbkdf2(string password, byte[] salt, int iterations, int outputBytes)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt)
            {
                IterationCount = iterations
            };
            return pbkdf2.GetBytes(outputBytes);
        }
    }
}
