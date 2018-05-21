using System;
using System.Web;

namespace Tmp.Web.Framework.Core
{
    /// <summary>
    /// Cookie控制表
    /// </summary>
    public class CookieManager
    {
        private CookieManager() { }

        public static void SetCookie(string name, string value, int time = 0, bool onlyread = true)
        {
            HttpCookie Cookie = new HttpCookie(name);
            if (time != 0)
                Cookie.Expires = DateTime.Now.AddDays(time);
            Cookie.Value = value;
            Cookie.HttpOnly = onlyread;
            System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
        }
        public static string GetCookie(string name)
        {
            HttpCookieCollection Cookie = HttpContext.Current.Request.Cookies;
            if (Cookie[name] != null)
                return Cookie[name].Value;
            return "";
        }
        public static void DeleteCookie(string name)
        {
            HttpCookie Cookie = new HttpCookie(name);
            Cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.SetCookie(Cookie);
        }
    }
}

