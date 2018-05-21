using System;
using System.Web;
using System.Text;
using System.Web.Caching;
using Tmp.Web.Framework.Core;
using System.Web.Mvc;
using Tmp.Core.Util;
using Tmp.Web.Framework.Filters;
namespace Tmp.Web.Framework.Controllers
{
    /// <summary>
    /// js、css资源合成器
    /// </summary>
    public class HttpCombinerController : BaseController
    {
        [Compress]
        public ContentResult Index()
        {
            string setName = Request["s"];
            string contentType = Request["t"];
            string version = Request["v"];
            string ex = Request["ex"];
            string requestRn = Request["Rn"];
            string cacheKey = "HttpCombinerEx." + setName + "." + version + "." + ex;
            TimeSpan CacheDuration = TimeSpan.FromDays(30);

            //假如浏览器有缓存，服务器缓存没有失效的话则返回304
            if(HttpContext.Cache[cacheKey] != null && Request.Headers["If-Modified-Since"] != null)
            {
                HttpContext.Response.StatusCode = 304;
                HttpContext.Response.StatusDescription = "Not Modified";
                return Content(String.Empty);
            }

            string result = (string)HttpContext.Cache[cacheKey];

            if ((result == null) || 0 == result.Length)
            {
                StringBuilder fileContents = new StringBuilder();
                string setDefinition = Convert.ToString((string.IsNullOrEmpty(ex) ? ConfigHelper.GetConfigString(setName) : ex));
                string[] fileNames = setDefinition.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] dependFiles = fileNames; //文件依赖，其中一个文件变更时Cache失效
                var nIndex = 0;

                foreach (string fileName in fileNames)
                {
                    fileContents.AppendLine(System.IO.File.ReadAllText(HttpContext.Server.MapPath(fileName.Trim())));
                    dependFiles[nIndex] = HttpContext.Server.MapPath(fileName);
                    nIndex = nIndex + 1;
                }

                HttpContext.Cache.Insert(
                    cacheKey,
                    fileContents.ToString(),
                    new CacheDependency(dependFiles),
                    Cache.NoAbsoluteExpiration,
                    TimeSpan.FromDays(30),
                    CacheItemPriority.High,
                    (string key, object value, CacheItemRemovedReason reason) =>
                    {
                        if ((requestRn != null))//此处不能直接操作request("Rn")，因为Lambda操作会再次失效
                            CombinerResource.Instance().ResetVersion(Request.RawUrl.Replace("&Rn=" + requestRn.ToString(), ""));
                        else
                            CombinerResource.Instance().ResetVersion(Request.RawUrl);
                    }
                );

                result = fileContents.ToString();
            }

            //设置HTTP响应头
            HttpContext.Response.Cache.SetCacheability(HttpCacheability.Public);
            HttpContext.Response.Cache.SetExpires(DateTime.Now.Add(CacheDuration));
            HttpContext.Response.Cache.SetLastModified(DateTime.Now);
            HttpContext.Response.Cache.SetMaxAge(CacheDuration);
            //HttpContext.Response.Cache.AppendCacheExtension("must-revalidate, proxy-revalidate");

            return Content(result, contentType, Encoding.UTF8);
           
        }
    }
}


