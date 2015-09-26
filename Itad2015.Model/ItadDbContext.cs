using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Workshop>()
                .HasMany(x => x.Guests)
                .WithOptional(x => x.Workshop)
                .HasForeignKey(x => x.WorkshopId);

            modelBuilder.Entity<WorkshopGuest>().HasRequired(x => x.Guest).WithMany().HasForeignKey(x => x.GuestId);

            base.OnModelCreating(modelBuilder);
        }

        class ItadDbInitializer : DropCreateDatabaseIfModelChanges<ItadDbContext>
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

                context.SaveChanges();
            }
        }
    }
}
