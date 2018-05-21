using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tmp.Core.Data;
using Tmp.Core.Util;
using Tmp.Service;
using XcBIMwebSys.Service.Interface;
using XcBIMwebSys.Service.Dto;
using Tmp.Data.Entity;

namespace XcBIMwebSys.WebCode.Controllers
{
    public class AccountController : BaseController
    {
        //private IAccountService _accountService;

        //public AccountController(IAccountService accountService)
        //{
        //    _accountService = accountService;
        //}

        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public string Search(AccountDto searchmodel)
        //{
        //    var list = _accountService.GteAccountList(searchmodel);
        //    return JSONHelper.ToJson(list, list.Count);
        //}

        //public ActionResult AddView()
        //{
        //    return View("Operate", new Account());
        //}

        //public ActionResult UpdateView(string accountID)
        //{
        //    var model = _accountService.GetModel(Guid.Parse(accountID));
        //    return View("Operate", model);
        //}

        //public ActionResult BatchAddView()
        //{
        //    return View();
        //}

        //public string BatchAdd(HttpPostedFileBase file)
        //{
        //    var dt = NPOIHelper.Import(file.InputStream, 0, 0);
        //    return _accountService.BatchAdd(dt) ? "1" : "0";
        //}
    }
}