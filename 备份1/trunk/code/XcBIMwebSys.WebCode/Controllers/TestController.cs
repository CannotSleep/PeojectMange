using System;
using System.Web.Mvc;
using Tmp.Web.Framework.Controllers;
using Tmp.Web.Framework.Filters;
using XcBIMwebSys.Service.Interface;

namespace XcBIMwebSys.WebCode.Controllers
{
    public class TestController : BaseController
    {
        private ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        // GET: Test
        public ActionResult Index()
        {
            ViewBag.Now = _testService.GetCurrentTime();
            return View();
        }
    }
}