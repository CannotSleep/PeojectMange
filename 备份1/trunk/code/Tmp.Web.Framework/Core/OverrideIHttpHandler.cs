using System;
using Tmp.Core.Data;
using Tmp.Core.Security;
using Tmp.Web.Framework.Models;
using System.Web;
using System.Web.SessionState;
using System.Text;

namespace Tmp.Web.Framework.Core
{
    /// <summary>
    /// 从旧框架转换而来
    /// todo待优化
    /// </summary>
    public class OverrideIHttpHandler : IHttpHandler, IRequiresSessionState
    {
        public OverrideIHttpHandler()
        {
        }

        public virtual void ProcessRequest(HttpContext context)
        {
        }

        public bool IsReusable
        {
            get { return false; }
        }
        ///// <summary>
        ///// 檢查操作權限
        ///// </summary>
        ///// <param name="context">页面查询的参数</param>
        ///// <remarks></remarks>
        //public void CheckPrivilege(HttpContext context)
        //{
        //    context.Response.ContentType = "application/json";
        //    context.Response.ContentEncoding = Encoding.Unicode;
        //    if (SessionManager.GetRoles() == null && SessionManager.GetLock() == null)
        //    {
        //        context.Response.Redirect("/Error/Detail?ErrorNo=" + ErrorTypeEnum.LostSession.ToString());
        //    }
        //    else
        //    {
        //       Utils.InitializeCulture();
        //    }
        //}


        #region "常用的方法"
        #endregion
    }
}
