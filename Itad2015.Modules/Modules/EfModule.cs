using Autofac;
using Itad2015.Repository;
using Itad2015.Repository.Common;

namespace Itad2015.Modules.Modules
{
    public class EfModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfDatabaseFactory>().As<IDatabaseFactory>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
        }
    }
}
