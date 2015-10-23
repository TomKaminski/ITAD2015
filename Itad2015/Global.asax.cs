using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using System.Web.Optimization;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Itad2015.FrontendMappings;
using Itad2015.FrontendModules;
using Itad2015.Modules.Infrastructure;
using Itad2015.Modules.Modules;

namespace Itad2015
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BaseFrontendMappings.Initialize();

            InitializerModule.InitializeDb();

            AddTask("RemoveUsers", 3600);

            var config = GlobalConfiguration.Configuration;

            //Autofac Configuration
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule(new MapperModule());
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ContractModule());
            builder.RegisterModule(new EfModule());
            builder.RegisterModule(new BaseFrontendModule());

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }


        private static void DeleteUsers()
        {
            InitializerModule.DeleteUsers();
        }


        private static CacheItemRemovedCallback _onCacheRemove;

        private void AddTask(string name, int seconds)
        {
            _onCacheRemove = CacheItemRemoved;
            HttpRuntime.Cache.Insert(name, seconds, null,
                DateTime.Now.AddSeconds(seconds), Cache.NoSlidingExpiration,
                CacheItemPriority.NotRemovable, _onCacheRemove);
        }

        private void CacheItemRemoved(string k, object v, CacheItemRemovedReason r)
        {
            DeleteUsers();
            AddTask(k, Convert.ToInt32(v));
        }
    }
}