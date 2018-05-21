using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tmp.Web.Framework.Filters
{
    /// <summary>
    /// CacheActionFilterAttribute 的摘要说明
    /// </summary>
    public class WebCacheAttribute : ActionFilterAttribute
    {

        public WebCacheAttribute()
        {
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.AppendHeader("Cache-Control", "public,must-revalidate, proxy-revalidate, max-age=2592000");
        }
    }
}