using System.Web;
using System.Web.Mvc;
using Tmp.Service;
using Tmp.Web.Framework.Core;
using Tmp.Service.Dto;

namespace COU.Web.Filter
{
    public class PemissionAuthorizeAttribute : AuthorizeAttribute
    {
        //todo
        private IPrivilegeService _privilegeService;
        private bool sessionUser =false;

        public IPrivilegeService PrivilegeService
        {
            get
            {
                return _privilegeService;
            }

            set
            {
                _privilegeService = value;
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (SessionManager.GetRoles() == null && SessionManager.GetLock() == null)
            {
                sessionUser = true;
                return false;
            }

            //页面权限控制
            var roleUser = SessionManager.GetRoles();
            var path = HttpContext.Current.Request.Path.ToString();

            var privilegeList = _privilegeService.GetUrlPermissionItems(roleUser.UserID, roleUser.RolesID);
            PrivilegeDto privilege = null;
            if (path != ""  && privilegeList != null &&  (privilege = privilegeList.Find(o => o.Url == path)) != null)
            {
                httpContext.Items["UrlPermission"] = privilege;
                return true;
            }
         
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (sessionUser)
            {
               // filterContext.HttpContext.Response.Redirect("/Error.aspx?Err=1");
                filterContext.Result = new RedirectResult("/Error/Detail?ErrorNo=1");
            }
            else
            {
                //filterContext.HttpContext.Response.Write("<script defer>window.alert('" + Resources.ResError.ErrDesc5 + "');history.back(-1);</script>");
                //filterContext.HttpContext.Response.Redirect("/Error.aspx?Err=5");
                filterContext.Result = new RedirectResult("/Error/Detail?ErrorNo=5");
            }
        }
    }
}