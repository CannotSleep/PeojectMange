using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Tmp.Web.Framework.Runtime;
using Autofac;
using Autofac.Integration.Mvc;
using System.Data.Entity;
using XcBIMwebSys.Service.Implement;
using System.Reflection;
using Tmp.Core.Dependency;
using Tmp.Web.Framework.Filters;
using XcBIMwebSys.WebCode.Controllers;
using Tmp.Data.Entity;
using Tmp.Service;

namespace XcBIMwebSys.WebCode
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes); 
            //注册过滤器

            Startup.Excute(builder => {
                List<Assembly> listAssembly = new List<Assembly>();
                var baseType = typeof(IDependency);
                listAssembly.Add(typeof(TestService).Assembly);
                listAssembly.Add(typeof(IErrorInfoService).Assembly);
                builder.RegisterAssemblyTypes(listAssembly.ToArray()).Where(t => baseType.IsAssignableFrom(t) & t != baseType).AsImplementedInterfaces().InstancePerLifetimeScope(); ;
                builder.RegisterControllers(typeof(TestController).Assembly);
                builder.RegisterType(typeof(MyDbContext)).As(typeof(DbContext)).InstancePerLifetimeScope();
            });

            //AutoMapper注册
            //AutoMapperConfig.Excute();
        }
    }
}
