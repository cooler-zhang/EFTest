using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.Injection;
using System.Configuration;
using Unity.AspNet.Mvc;
using Unity.Lifetime;

namespace EFTest.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            InitIoC();
        }

        public static void InitIoC()
        {
            DependencyResolver.SetResolver(new UnityDependencyResolver(ContainerManager.Current));

            var resolveDBNameFunc = new Func<string>(() =>
            {
                var host = HttpContext.Current.Request.Url.Host;
                return host.IndexOf(".") > 0 ? host.Substring(0, host.IndexOf(".")) : host;
            });
            //PerCallContextLifeTimeManager PerThreadLifetimeManager
            ContainerManager.Current.RegisterType<MyDbContext>(new PerCallContextLifeTimeManager(), new InjectionConstructor(resolveDBNameFunc));
            ContainerManager.Current.RegisterType<ClassRepository>();
            ContainerManager.Current.RegisterType<StudentRepository>();
            ContainerManager.Current.RegisterType<StudentService>();
            ContainerManager.Current.RegisterType<ProductService>();
        }
    }
}
