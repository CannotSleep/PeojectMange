using Autofac;
using System;
using System.Linq;
using Tmp.Core.Dependency;
using Autofac.Integration.Mvc;
using Tmp.Data.Entity;
using Tmp.Core.Data;
using Tmp.Web.Framework.Core;
using System.Web.Mvc;
using AutoMapper;
using Tmp.Web.Framework.Models;
//using Tmp.Service.Dto;
using Tmp.Core.Security;
using Tmp.Core.Caching;
using System.Web.Routing;

namespace Tmp.Web.Framework.Runtime
{
    public class Startup
    {
        public static void Excute(Action<ContainerBuilder> IocRegisterFun)
        {
            var builder = new ContainerBuilder();
            var baseType = typeof(IDependency);
            var assemblys = AppDomain.CurrentDomain.GetAssemblies().ToList().Where(t => { return t.FullName.Contains("Controllers") | t.FullName.Contains("Filters") | t.FullName.Contains("Models") | t.FullName.Contains("BLL") | t.FullName.Contains("DAL") | t.FullName.Contains("Tmp") | t.FullName.Contains("App_Code"); });

            builder.RegisterControllers(assemblys.ToArray());
            builder.RegisterFilterProvider();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(assemblys.ToArray()).Where(t => baseType.IsAssignableFrom(t) & t != baseType).AsImplementedInterfaces().InstancePerLifetimeScope();
            IocRegisterFun(builder);

            //注入Cache
            //builder.RegisterGeneric(typeof(HttpContextCacheManager)).As(typeof(IHttpContextCacheManager)).InstancePerLifetimeScope();
            //builder.RegisterGeneric(typeof(HttpContextSessionManager)).As(typeof(IHttpContextSessionManager)).InstancePerLifetimeScope();
            //builder.RegisterGeneric(typeof(HttpContextSessionManager)).As(typeof(IHttpContextCacheManager)).InstancePerLifetimeScope();


            //设置相关全局变量
            IocContainerManager.SetInstance(builder.Build());
            IocObjectManager.SetInstance(new IocObjectManager(new IocLifetimeScope()));
            DependencyResolver.SetResolver(new AutofacDependencyResolver(IocContainerManager.GetInstance()));

            //RazorViewEngine设置
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new TmpRazorViewEngine());

            AutoMapperConfig();
            RouteConfig();
        }

        /// <summary>
        /// 路由设置
        /// </summary>
        public static void RouteConfig()
        {
            RouteTable.Routes.MapRoute("Default2", "HttpCombiner", new { controller = "HttpCombiner", action = "Index", id = UrlParameter.Optional });
        }

        /// <summary>
        /// 设置模型转换
        /// </summary>
        public static void AutoMapperConfig()
        {

            Func<GridParams, string> fun = (GridParams gridParams) =>
            {
                var fieldList = "";
                if (gridParams.Columns != null)
                {
                    gridParams.Columns.ForEach(p => fieldList += p.Field + ",");
                    fieldList = fieldList.Substring(0, fieldList.Length - 1);
                }
                else
                {
                    fieldList = "*";
                }
                return fieldList;
            };

            
            Mapper.CreateMap<GridParams, Tmp.Core.Data.PageParam>()
                .ForMember(d => d.FieldList, opt => { opt.MapFrom(s => fun(s)); })
                .ForMember(d => d.pageSize, opt => { opt.MapFrom(s => s.PageSize); })
                .ForMember(d => d.pageIndex, opt => { opt.MapFrom(s => s.PageIndex); })
                .ForMember(d => d.TotalCount, opt => { opt.MapFrom(s => s.TotalCount); })
                .ForMember(d => d.Order, opt => { opt.MapFrom(s => s.SortField + " " + s.SortDirection); });
        }
    }
}
