using System;
using Tmp.Core.Data;
using Tmp.Core.Security;
using Tmp.Service;
using Tmp.Data.Entity;
using Tmp.Core.Dependency;
using System.Web;
using System.Text;
using Tmp.Web.Framework.Runtime;

namespace Tmp.Web.Framework.Core
{
    /// <summary>
    /// HttpModuleErrorHandler用于处理页面未捕获异常，使用于Aspx和Ashx
    /// Add 2015-11-1
    /// </summary>
    /// <remarks></remarks>
    public class HttpModuleErrorHandler : IHttpModule
    {


        public void Dispose()
        {
        }


        public void Init(HttpApplication context)
        {

            context.Error += (object sender, System.EventArgs e) =>
            {
                var container = IocContainerManager.GetInstance();

                if (((container != null)) & (HttpContext.Current.Items["PerRequestScope"] == null))
                {
                    var scope = container.BeginLifetimeScope();
                    HttpContext.Current.Items["PerRequestScope"] = scope;
                }

                var Request = context.Request;
                var Response = context.Response;
                Exception currentError = context.Server.GetLastError();
                var bllErrorInfo = new ErrorInfoService();
                var errorInfo = new ErrorInfo();
                //if ((SessionManager.GetUser() != null))
                //{
                //    errorInfo.UID = SessionManager.GetUser().AccountID;
                //}
                errorInfo.ErrorMsg = currentError.Message.ToString();

                if (currentError.GetType().Name == "SysDbException")
                {
                    SysDbException ex = (SysDbException)currentError;
                    errorInfo.ExecSql = ex.SqlInfo;
                    errorInfo.ErrorCode = 1;
                }

                errorInfo.ProgramID = "0";
                errorInfo.Url = Request.Url.ToString();
                errorInfo.StackTrace = currentError.ToString();
                StringBuilder sb = new StringBuilder();
                Response.Clear();
                sb.Append("<h2>系统错误：</h2><hr/>系统发生错误，错误编号为：");

                if (!errorInfo.Url.Contains("/Error"))
                {
                    sb.Append(bllErrorInfo.Add(errorInfo));
                }
                //todo对于已记录的无需再记录
                sb.Append("。该信息已被系统记录，请稍后重试或与系统管理员联系。<br/>错误地址：");
                sb.Append(Request.Url.ToString());
                sb.Append("<br/>错误信息： <font class='ErrorMessage'>");
                sb.Append(currentError.Message.ToString());
                sb.Append("</font><hr/><b>Stack Trace:</b><br/>");
                sb.Append(currentError.ToString());
                Response.Write(sb.ToString());
                Response.End();
                context.Server.ClearError();
            };
        }
    }
}
