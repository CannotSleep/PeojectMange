using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tmp.Data.Entity;
using XcBIMwebSys.Service.Interface;

namespace XcBIMwebSys.WebCode.Controllers
{
    public class ProjectController : Controller
    {
        private IProjectInfoService _projectInfoService;

        public ProjectController(IProjectInfoService projectInfoService)
        {
            _projectInfoService = projectInfoService;
        }
        // GET: Project
        public ActionResult Index()
        {
            var projects = _projectInfoService.UseRepository.Table.ToList();
            return View(projects);
        }

        public JsonResult Add()
        {
            ProjectInfo projectInfo = new ProjectInfo
            {
                ID = Guid.NewGuid(),
                AccountID = Guid.NewGuid(),
                ProjectName = "Name",
                ProjectType = 1,
                ProjectAddress = "GUANGZHOU",
                ProjectSbeummary = "111",
                StartTime = DateTime.Parse("2018-01-12"),
                EndTime = DateTime.Parse("2018-06-09"),
                CreateTime = DateTime.Now  
            };
            _projectInfoService.UseRepository.Insert(projectInfo);
            return Json("add success!", JsonRequestBehavior.AllowGet);
        }
    }
}