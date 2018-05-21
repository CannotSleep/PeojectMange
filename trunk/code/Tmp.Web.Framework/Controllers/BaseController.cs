using System;
using System.Web.Mvc;
using Tmp.Core.Data;
using Tmp.Core.Dependency;
using Tmp.Core.Util;
using Tmp.Data.Entity;
using Autofac;
using Tmp.Service;
using Tmp.Web.Framework.Core;
using Tmp.Web.Framework.Filters;
using System.Collections.Generic;
using Tmp.Web.Framework.Runtime;

namespace Tmp.Web.Framework.Controllers
{
    [CustomHandleError]
    public class BaseController : Controller
    {
        //protected virtual void LogInfo(string userId, string infoMsg)
        //{
        //    var errorInfo = new ErrorInfo();
        //    errorInfo.ErrorMsg = infoMsg;
        //    errorInfo.RunningTime = System.DateTime.Now;
        //    errorInfo.Url = HttpContext.Request.Path.ToString();
        //    _errorInfoService.Add(errorInfo);
        //}

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            //todo完善
            //创建基于http request的scope用于显式的依赖注入,eg RespositoryFactory CouDbContext的构建
            var container = IocContainerManager.GetInstance();
            if (container != null)
            {
                var scope = container.BeginLifetimeScope();
                HttpContext.Items["PerRequestScope"] = scope;
            }

            Utils.InitializeCulture();
            return base.BeginExecuteCore(callback, state);
        }


        public String GetModelInvalidMsg()
        {
            string strError = "";

            foreach (var e in ModelState)
            {
                if (e.Value.Errors.Count > 0)
                {
                    strError += e.Key.ToString() + ":" + e.Value.Errors[0].ErrorMessage;
                }
            }

            return strError;
        }


        public GridParams GetGridParams()
        {
            var grid = new GridParams();

            if (HttpContext.Request["rows"] != null)
            {
                grid.PageSize = Int32.Parse(HttpContext.Request["rows"].ToString());
            }

            if (grid.PageSize == 0)
            {
                grid.PageSize = 10;
            }
                
            if (HttpContext.Request["page"] != null)
            {
                grid.PageIndex = Int32.Parse(HttpContext.Request["page"].ToString());

                if (grid.PageIndex == 0)
                {
                    grid.PageIndex = 0;
                }
            }

            if (HttpContext.Request["sort"] != null)
            {
                grid.SortField = HttpContext.Request["sort"].ToString();
            }

            if (HttpContext.Request["order"] != null)
            {
                grid.SortDirection = HttpContext.Request["order"].ToString();
            }

            if (HttpContext.Request["total"] != null && HttpContext.Request["total"] != "NaN")
            {
                grid.TotalCount = int.Parse(HttpContext.Request["total"]);
            }

            if(HttpContext.Request["gridFields"] != null)
            {
                grid.Columns = JSONHelper.FromJsonTo<List<GridColumn>>(HttpContext.Request["gridFields"].ToString());
            }

            return grid;
        }
    }
}

