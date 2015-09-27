using Autofac;
using Itad2015.Helpers.Email;

namespace Itad2015.FrontendModules
{
    public class BaseFrontendModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EmailHelper<>)).As(typeof(IEmailHelper<>));
        }
    }
}