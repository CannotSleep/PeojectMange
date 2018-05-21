using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using XcBIMwebSys.Service.Interface;
using XcBIMwebSys.Service.Tools;

namespace XcBIMwebSys.WebCode.Controllers
{
    public class MaterialController : Controller
    {
        private IMaterialService _materiaService;


        public MaterialController(IMaterialService materiaService)
        {
            _materiaService = materiaService;
        }

        // GET: Material
        public ActionResult Index()
        {
            return View();
        }

        // GET: File
        public ActionResult ViewPic()
        {
            return View();
        }

        // GET: File
        public ActionResult DocView()
        {
            return View();
        }

        // GET: File
        public ActionResult UploadView()
        {
            return View();
        }
        // GET: File
        public ActionResult GetExcelView()
        {
            return View();
        }

        //文件上传action
        //2018/5/4
        //zyt初次编写
        [HttpPost]
        public string UploadFile(HttpPostedFileBase file)
        {

            //HttpPostedFileBase file = Request.Files["myfile"];

            string fileType = Request.Params["type"];

            string fileExplain = Request.Params["introduce"];

            MaterialTool fillTool = new MaterialTool();

            int type = fillTool.getFileType(file.FileName);

            var fileName = file.FileName;

            var filePath = Server.MapPath("~/Temp/" + "123654");

            int size = file.ContentLength;

            string username = "1user";

            int userid = 111;

            string project = "项目a";

            int projectid = 123;

            //Stream s = System.Web.HttpContext.Current.Request.InputStream;
            //byte[] b = new byte[s.Length];
            //s.Read(b, 0, (int)s.Length);
            //string str= Encoding.UTF8.GetString(b);

            //string fileType = Request.QueryString[0];

            //string fileType="分类示例";

            //string fileExplain = Request.Params["desc"];

            //string fileExplain="说明示例";

            string result =  _materiaService.UploadFile(file,username,userid,filePath,fileName,project,projectid,size,type,fileType,fileExplain);

            switch (result) {
                case "上传成功":
                    return 1+"";
                case "上传失败":
                    return 0+"";
                default:
                    break;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(0);

        }



        //获取文件数据库列表
        //2018-5-7
        //zyt初次编写
        [HttpGet]
        public string getfileList()
        {

            string page =  Request.Params["page"];

            string limit = Request.Params["limit"];

            string userName = Request.Params["userName"];

            string fileName = Request.Params["fileName"];

            string fileType = Request.Params["projectType"];

            int a = 1;
            if (userName == "" || fileName == "" || fileType == ""||userName==null||fileName==null||fileType==null)
            {
                a = 0;
            }

            if (userName==null||userName=="") {
                userName = ".";
            }
            if (fileName==null||fileName=="") {
                fileName = ".";
            }
            if (fileType==null||fileType=="") {
                fileType = ".";
            }
          

            var orderLs = _materiaService.getFileList(page,limit, userName,fileName,fileType,a);

            string count = _materiaService.getTotal("MaterialInfo", userName, fileName, fileType,a);

            string dd = Newtonsoft.Json.JsonConvert.SerializeObject(orderLs).ToString();

            string result = "{"+'"'+"code"+'"'+':'+"0,"+'"'+"msg"+'"'+':'+'"'+'"'+','+'"'+"count"+'"'+':'+ count + ','+'"'+"data"+'"'+':'+dd+"}";


            return result;
        }

        ////获取文件数据库列表
        ////2018-5-7
        ////zyt初次编写
        //[HttpGet]
        //public string getfileListPage(string page,string limit)
        //{

        //    var orderLs = _materiaService.getFileList(page,limit);
        //    int count = orderLs.Count();

        //    string dd = Newtonsoft.Json.JsonConvert.SerializeObject(orderLs).ToString();

        //    string result = "{" + '"' + "code" + '"' + ':' + "0," + '"' + "msg" + '"' + ':' + '"' + '"' + ',' + '"' + "count" + '"' + ':' + count + ',' + '"' + "data" + '"' + ':' + dd + "}";


        //    return result;
        //}



        /// <summary>
        /// 根据文件名称删除文件
        /// </summary>
        [HttpGet]
        public int deleteFileByFileName(string name)
        {

            var filePath = Server.MapPath("~/Temp/" + "123654"+"/");

            filePath = filePath + name;

            if (_materiaService.deleteFile(filePath, name))
            {
                return 1;
            }
            else {
                return 0;
            }

            
        }


        /// <summary>
        /// 下载文件方法
        /// </summary>
        /// 
        
        public FileResult downloadFile()
        {
            string filePath = "";

            string fileName = Request["fileName"];

            string[] strArray = fileName.Split('-');

            string fileName2 = strArray[0];

            string path1 = Server.MapPath("~/Temp/" + "123654/");

            filePath = path1 + fileName;


            return File(_materiaService.downFile(filePath), "application/octed-stream", HttpContext.Request.Browser.Browser == "IE" ? Url.Encode(fileName2) : fileName2);
        }



        /// <summary>
        /// 下载文件方法
        /// </summary>
        /// 
        [HttpGet]
        public string ReadExcel()
        {

            _materiaService.readExcel("");

            return null;
        }
    }
}
