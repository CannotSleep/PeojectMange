using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Tmp.Core.Dependency;
using Tmp.Data.Entity;
using Tmp.Service;
using Tmp.Web.Framework.Core;


namespace Tmp.Web.Framework.Filters
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    /// <summary>
    /// CustomHandleErrorAttribute 用于处理并记录系统错误
    /// </summary>
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        //IErrorInfoService _errorInfoService;
        //public IErrorInfoService ErrorInfoService
        //{
        //    get{return _errorInfoService;}
        //    set {_errorInfoService = value;}
        //}

        //public override void OnException(ExceptionContext filterContext)
        //{
        //    if (!filterContext.ExceptionHandled)
        //    {
        //        string controllerName = (string)filterContext.RouteData.Values["controller"];
        //        string actionName = (string)filterContext.RouteData.Values["action"];

        //        if (filterContext.Exception.GetType().Name == "CustomerException")
        //        {
        //            HandleErrorInfo info = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
        //            filterContext.Result = new ViewResult()
        //            {
        //                ViewName = "~/Views/Shared/Error.cshtml",
        //                ViewData = new ViewDataDictionary<HandleErrorInfo>(info)
        //            };
        //            filterContext.ExceptionHandled = true;
        //            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        //        }

                
        //        var bllErrorInfo = IocObjectManager.GetInstance().Resolve<IErrorInfoService>();
        //        var errorInfo = new ErrorInfo();
        //        //if ((SessionManager.GetUser() != null))
        //        //{
        //        //    errorInfo.UID = SessionManager.GetUser().AccountID;
        //        //}
        //        //else
        //        //    errorInfo.UID = "";
        //        errorInfo.RunningTime = DateTime.Now;
        //        errorInfo.ErrorMsg = filterContext.Exception.Message;
        //        errorInfo.ErrorCode = 0;
        //        errorInfo.ProgramID = "0";
        //        errorInfo.Url = HttpContext.Current.Request.Url.ToString();
        //        errorInfo.StackTrace = filterContext.Exception.StackTrace;
        //        errorInfo.SolveBy = "";
        //        StringBuilder sb = new StringBuilder();
        //        HttpContext.Current.Response.Clear();
        //        sb.Append("<h2>系统错误：</h2><hr/>系统发生错误，错误编号为：");
        //        sb.Append(bllErrorInfo.Add(errorInfo));
        //        sb.Append("。该信息已被系统记录，请稍后重试或与系统管理员联系。<br/>错误地址：");
        //        sb.Append(HttpContext.Current.Request.Url.ToString());
        //        sb.Append("<br/>错误信息： <font class='ErrorMessage'>");
        //        sb.Append(filterContext.Exception.Message);
        //        sb.Append("</font><hr/><b>Stack Trace:</b><br/>");
        //        sb.Append(filterContext.Exception.StackTrace);
        //        HttpContext.Current.Response.Write(sb.ToString());
        //        HttpContext.Current.Response.End();

        //    }
        //}
    }
}
