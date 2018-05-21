using System;
using Tmp.Core.Dependency;
using Tmp.Core.Security;
using Tmp.Service;
using Tmp.Data.Entity;
using Tmp.Web.Framework.Models;
using System.Web.UI;
using System.Web;
using System.Text;
using Tmp.Web.Framework.Runtime;

namespace Tmp.Web.Framework.Core
{

    public class PageBase : Page
    {
        public PageBase()
        {
        }
        /// <summary>
        /// 引发 <see cref="E:System.Web.UI.Control.Init" /> 事件以对页进行初始化。
        /// </summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.EventArgs" />。</param>

        protected override void OnInit(EventArgs e)
        {
            //创建基于http request的scope用于显式的依赖注入,eg RespositoryFactory CouDbContext的构建
            var container = IocContainerManager.GetInstance();
            if ((container != null))
            {
                dynamic scope = container.BeginLifetimeScope();
                HttpContext.Current.Items["PerRequestScope"] = scope;
            }

            base.OnInit(e);
            Load += new EventHandler(PageBase_Load);
            Error += new EventHandler(PageBase_Error);
        }
        /// <summary>
        /// Handles the Error event of the PageBase control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void PageBase_Error(object sender, System.EventArgs e)
        {
            Exception currentError = Server.GetLastError();
            var bllErrorInfo = IocObjectManager.GetInstance().Resolve<IErrorInfoService>();
            var errorInfo = new ErrorInfo();
            //if ((SessionManager.GetUser() != null))
            //{
            //    errorInfo.UID = SessionManager.GetUser().AccountID;
            //}
            errorInfo.ErrorMsg = currentError.Message.ToString();
            errorInfo.ProgramID = "0";
            errorInfo.Url = Request.Url.ToString();
            errorInfo.StackTrace = currentError.ToString();
            StringBuilder sb = new StringBuilder();
            Response.Clear();
            sb.Append("<h2>系统错误：</h2><hr/>系统发生错误，错误编号为：");
            sb.Append(bllErrorInfo.Add(errorInfo));
            sb.Append("。该信息已被系统记录，请稍后重试或与系统管理员联系。<br/>错误地址：");
            sb.Append(Request.Url.ToString());
            sb.Append("<br/>错误信息： <font class='ErrorMessage'>");
            sb.Append(currentError.Message.ToString());
            sb.Append("</font><hr/><b>Stack Trace:</b><br/>");
            sb.Append(currentError.ToString());
            Response.Write(sb.ToString());
            Response.End();
            Server.ClearError();
        }
        public virtual int GetPermissionID()
        {
            //默认-1为限制，可以在不同页面重写里来控制不同页面的权限
            return -1;
        }

        public virtual bool VerifyUser()
        {
            //默认true为限制，可以在不同页面重写里来控制是否验证用户存在性
            return true;
        }

        /// <summary>
        /// Handles the Load event of the PageBase control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void PageBase_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //if (VerifyUser() == true & SessionManager.GetRoles() == null && SessionManager.GetLock() == null)
                //{
                //    Response.Redirect("/Error/Detail?ErrorNo=" + ErrorTypeEnum.LostSession.ToString());
                //}
                //else
                //{
                //    if (GetPermissionID() == -1)
                //    {
                //        //判断只有当父窗口有进度条时才关闭进度条
                //        Response.Write("<script>if(typeof(parent.$) != 'undefined' && typeof(parent.$.messager) != 'undefined')parent.$.messager.progress('close');</script>");

                //        //控制页面权限  todo
                //    }
                //}
            }
        }
        /// <summary>
        /// 国际化
        /// </summary>
        /// <remarks></remarks>
        protected override void InitializeCulture()
        {
            Utils.InitializeCulture();
        }
    }



}
