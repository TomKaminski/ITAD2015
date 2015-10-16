using Autofac;
using Itad2015.Repository.Concrete;
using Itad2015.Repository.Interfaces;
using Module = Autofac.Module;

namespace Itad2015.Modules.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GuestRepository>().As<IGuestRepository>();
            builder.RegisterType<WorkshopRepository>().As<IWorkshopRepository>();
            builder.RegisterType<WorkshopGuestRepository>().As<IWorkshopGuestRepository>();
            builder.RegisterType<PrizeRepository>().As<IPrizeRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<InvitedPersonRepository>().As<IInvitedPersonRepository>();
        }
    }
}
