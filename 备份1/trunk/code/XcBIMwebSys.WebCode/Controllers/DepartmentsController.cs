using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tmp.Core.Util;

namespace XcBIMwebSys.WebCode.Controllers
{
    public class DepartmentsController : Controller
    {
        // GET: Departments
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult BatchImport(HttpPostedFileBase file)
        {
            var dt = NPOIHelper.Import(file.InputStream, 0, 0);

            return View();
        }
    }
}