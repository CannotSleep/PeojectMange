using System.Web;
using System.Web.Mvc;
using Tmp.Service;
using Tmp.Web.Framework.Core;
using Tmp.Web.Framework.Models;

namespace Tmp.Web.Framework.Filters
{
    /// <summary>
    /// LoginAuthorizeAttribute 的摘要说明
    /// </summary>
    public class LoginAuthorizeAttribute : AuthorizeAttribute
    {
        //tido
       

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            
            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = filterContext.HttpContext.Request.UrlReferrer == null ? 
                new RedirectResult("/Account/Login") : 
                new RedirectResult("/Error/Detail?ErrorNo="+ ErrorTypeEnum.LostSession);
        }
    }
}
