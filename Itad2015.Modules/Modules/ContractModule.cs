using Autofac;
using Itad2015.Contract.Service;
using Itad2015.Contract.Service.Entity;
using Itad2015.Service.Concrete;
using Itad2015.Service.Helpers;
using Itad2015.Service.Helpers.Interfaces;

namespace Itad2015.Modules.Modules
{
    public class ContractModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GuestService>().As<IGuestService>();
            builder.RegisterType<PdfService>().As<IPdfService>();
            builder.RegisterType<PrizeService>().As<IPrizeService>();
            builder.RegisterType<WorkshopService>().As<IWorkshopService>();
            builder.RegisterType<WorkshopGuestService>().As<IWorkshopGuestService>();
            builder.RegisterType<UserService>().As<IUserService>();


            builder.RegisterType<PasswordHasher>().As<IPasswordHasher>();
            builder.RegisterType<QRCodeGenerator>().As<IQrCodeGenerator>();
            builder.RegisterType<ImageProcessorService>().As<IImageProcessorService>();
            builder.RegisterType<HashGenerator>().As<IHashGenerator>();

            builder.RegisterGeneric(typeof(CustomExpressionVisitor<>)).AsSelf();
        }
    }
}
