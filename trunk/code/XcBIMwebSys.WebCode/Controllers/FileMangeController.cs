using Newtonsoft.Json;
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
    public class FileMangeController : Controller
    {
        private IFileMangeService _materiaService;


        public FileMangeController(IFileMangeService materiaService)
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

            string FileCategory = Request.Params["category"];

            string FileExplain = Request.Params["introduce"];

            FileMangeTool fillTool = new FileMangeTool();

            string FileType = fillTool.getFileType(file.FileName);

            var NameBefore = file.FileName;

            string ProjectName = "测试项目一";

            string UserName = "1234";       

            int Size = file.ContentLength;

            string userid = "204ac654-81cc-4248-b82d-92b9d705896d";

            Guid UserId = new Guid(userid);

            string projectid = "0ac4eae8-28fa-4bf3-a09a-13c9932b1710";

            Guid PeojectId = new Guid(projectid);

            var FilePath = Server.MapPath("~/Temp/" + ProjectName + "/" + UserName);
            //Stream s = System.Web.HttpContext.Current.Request.InputStream;
            //byte[] b = new byte[s.Length];
            //s.Read(b, 0, (int)s.Length);
            //string str= Encoding.UTF8.GetString(b);

            //string fileType = Request.QueryString[0];

            //string fileType="分类示例";

            //string fileExplain = Request.Params["desc"];

            //string fileExplain="说明示例";

            string result =  _materiaService.UploadFile(file, NameBefore, Size, UserId, FileType, FilePath, PeojectId, FileCategory, FileExplain, UserName, ProjectName);
            switch (result) {
                case "上传成功":
                    return Newtonsoft.Json.JsonConvert.SerializeObject(1);
                case "上传失败":
                    return Newtonsoft.Json.JsonConvert.SerializeObject(0);
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
          

            var orderLs = _materiaService.getFileList(page,limit,fileName,fileType,a);

            string count = _materiaService.getTotal("FileMange", fileName, fileType,a);
           
            //string dd = Newtonsoft.Json.JsonConvert.SerializeObject(orderLs).ToString();

            string dd= JsonConvert.SerializeObject(orderLs, Formatting.None,new JsonSerializerSettings() {
                ReferenceLoopHandling=ReferenceLoopHandling.Ignore
            });

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
