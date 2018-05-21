using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualBasic;
using System.Threading;
using Tmp.Service;
using Tmp.Web.Framework.Core;
using Tmp.Web.Framework.Filters;
using System;

namespace Tmp.Web.Framework.Controllers
{ 
    /// <summary>
    /// HomeController 的摘要说明
    /// </summary>
    [LoginAuthorize]
    public class HomeController : BaseController
    {

        public HomeController()
        {

        }


        public ActionResult Index()
        {
            //菜单简繁体转换
            var localID = Thread.CurrentThread.CurrentUICulture.Name == "zh-CN" ? 0 : 2;
            VbStrConv strTransfer = localID == 0? Microsoft.VisualBasic.VbStrConv.SimplifiedChinese : Microsoft.VisualBasic.VbStrConv.TraditionalChinese;
            

            return View();
        }
    }

}