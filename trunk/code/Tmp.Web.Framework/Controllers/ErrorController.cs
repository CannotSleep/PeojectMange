using System.Web.Mvc;
using Tmp.Web.Framework.Models;
using Tmp.Data.Entity;
using Tmp.Service;
using System;
using Tmp.Web.Framework.Core;
using Tmp.Core;

namespace Tmp.Web.Framework.Controllers
{
    public class ErrorController : BaseController
    {
        private IErrorInfoService _errorInfoService;

        public ErrorController(IErrorInfoService errorInfoService)
        {
            _errorInfoService = errorInfoService;
        }

        /// <summary>
        /// 显示错误详情
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Detail(ErrorModel model)
        {
            var errorMsg = "";
            var redirectUrl = "";
            switch (model.ErrorNo)
            {
                //case ErrorTypeEnum.LostSession:
                //    errorMsg = ResError.Tooltip1 + ResError.ErrDesc1;
                //    redirectUrl = "/Account/Login";
                //    break;
                case ErrorTypeEnum.ServerConnectionFailed:
                    errorMsg = "系统温馨提示您:连接服务器失败,请联系管理员!";
                    break;
                case ErrorTypeEnum.NotAccessPage:
                    errorMsg = "系统温馨提示您:无权访问该页面!";
                    break;
                case ErrorTypeEnum.PageNotFound:
                    errorMsg = "系统温馨提示您:找不到网页!";
                    break;
                default:
                    errorMsg = "系统温馨提示您:访问出错,请联系管理员!";
                    break;
            }
            model.ErrorMsg = string.IsNullOrEmpty(model.ErrorMsg) ? errorMsg : model.ErrorMsg;
            model.RedirectUrl = string.IsNullOrEmpty(model.RedirectUrl) ? redirectUrl : model.RedirectUrl;
            return View(model);
        }

        /// <summary>
        /// 保存错误
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //[HttpPost]
        //[ValidateInput(false)]
        //public JsonResult Add(ErrorInfo model)
        //{
        //    model.Url = Request.UrlReferrer.ToString();
        //    model.RunningTime = DateTime.Now;
        //    //model.UID = SessionManager.GetUser().AccountID;
        //    _errorInfoService.Add(model);
        //    return Json(new ResultInfo() { ErrorNo = 0, ErrorMsg = "" });
        //}
    }
}
