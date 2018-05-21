using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XcBIMwebSys.WebCode.Controllers
{
    public class MaterialController : Controller
    {
        //文件数据库操作类
        API.DAL.fileOne fone = new API.DAL.fileOne();


        // GET: File
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

        //文件上传action
        //2018/5/4
        //zyt初次编写
        [HttpPost]
        public string UploadFile()
        {

            HttpPostedFileBase file = Request.Files["myfile"];


            FillTool fillTool = new FillTool();


            string date = fillTool.getNowDate();

            int type = fillTool.getFileType(file.FileName);

            //文件名添加guid防止文件名重复
            var gu = Guid.NewGuid();


            var fileName = file.FileName;
            var filePath = Server.MapPath("~/Temp/" + "username");
            int size = file.ContentLength;



            //根据用户名创建文件夹
            if (System.IO.Directory.Exists(Server.MapPath("~/Temp/" + "username")) == false)//如果不存在就创建file文件夹
            {
                System.IO.Directory.CreateDirectory(Server.MapPath("~/Temp/" + "username"));
            }

            if (System.IO.File.Exists(filePath + "/" + fileName))
            {

                return "已存在";
            }


            file.SaveAs(Path.Combine(filePath, fileName));

            //decimal size = fillTool.getDirectorySize(filePath + "/" + fileName, "MB");

            //文件实体类
            API.Model.fileOne fileOne = new API.Model.fileOne();
            fileOne.tb_int = gu;
            fileOne.tb_name_before = file.FileName;
            fileOne.tb_name_now = fileName;
            fileOne.tb_date = date;
            fileOne.tb_size = size;
            fileOne.tb_username = "username";
            fileOne.tb_userid = 1;
            fileOne.tb_type = type;
            fileOne.tb_address = filePath;
            fileOne.tb_projectid = 2;
            fileOne.tb_project = "b";


            fone.AddB(fileOne);


            return "上传成功";
        }


        //获取文件数据库列表
        //2018-5-7
        //zyt初次编写
        [HttpGet]
        public string fileList()
        {
            DataSet dateSet = fone.GetListB("");


            List<API.Model.fileOne> list = DataTableToList(dateSet.Tables[0]);

            var orderLs = list.OrderByDescending(a => a.tb_date).ToList();

            return Newtonsoft.Json.JsonConvert.SerializeObject(orderLs);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<API.Model.fileOne> DataTableToList(DataTable dt)
        {
            List<API.Model.fileOne> modelList = new List<API.Model.fileOne>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                API.Model.fileOne model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public API.Model.fileOne DataRowToModel(DataRow row)
        {
            API.Model.fileOne model = new API.Model.fileOne();
            if (row != null)
            {
                if (row["tb_int"] != null && row["tb_int"].ToString() != "")
                {
                    model.tb_int = new Guid(row["tb_int"].ToString());
                }

                if (row["tb_name_before"] != null && row["tb_name_before"].ToString() != "")
                {
                    model.tb_name_before = row["tb_name_before"].ToString();
                }

                if (row["tb_name_now"] != null && row["tb_name_now"].ToString() != "")
                {
                    model.tb_name_now = row["tb_name_now"].ToString();
                }

                if (row["tb_date"] != null && row["tb_date"].ToString() != "")
                {
                    model.tb_date = row["tb_date"].ToString();
                }

                if (row["tb_size"] != null && row["tb_size"].ToString() != "")
                {
                    model.tb_size = int.Parse(row["tb_size"].ToString());
                }

                if (row["tb_username"] != null && row["tb_username"].ToString() != "")
                {
                    model.tb_username = row["tb_username"].ToString();
                }

                if (row["tb_userid"] != null && row["tb_userid"].ToString() != "")
                {
                    model.tb_userid = int.Parse(row["tb_userid"].ToString());
                }

                if (row["tb_type"] != null && row["tb_type"].ToString() != "")
                {
                    model.tb_type = int.Parse(row["tb_type"].ToString());
                }

                if (row["tb_address"] != null && row["tb_address"].ToString() != "")
                {
                    model.tb_address = row["tb_address"].ToString();
                }

                if (row["tb_projectid"] != null && row["tb_projectid"].ToString() != "")
                {
                    model.tb_projectid = int.Parse(row["tb_projectid"].ToString());
                }

                if (row["tb_project"] != null && row["tb_project"].ToString() != "")
                {
                    model.tb_project = row["tb_project"].ToString();
                }
            }
            return model;
        }


        /// <summary>
        /// 根据项目id获取材料列表
        /// </summary>
        [HttpGet]
        public string getFileListByProjectId(int id)
        {
            DataSet dateSet = fone.GetList(" tb_projectid = " + id);


            List<API.Model.fileOne> list = DataTableToList(dateSet.Tables[0]);

            var orderLs = list.OrderByDescending(a => a.tb_date).ToList();

            return Newtonsoft.Json.JsonConvert.SerializeObject(orderLs);
        }

        /// <summary>
        /// 根据项目名称获取获取材料列表
        /// </summary>
        [HttpGet]
        public string getFileListByProjectName(string name)
        {

            DataSet dateSet;
            if (name != "")
            {
                dateSet = fone.GetList(" CHARINDEX('" + name + "',tb_name_before)>0");
            }
            else
            {
                dateSet = fone.GetList("");
            }


            List<API.Model.fileOne> list = DataTableToList(dateSet.Tables[0]);

            var orderLs = list.OrderByDescending(a => a.tb_date).ToList();

            return Newtonsoft.Json.JsonConvert.SerializeObject(orderLs);
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



            string path1 = Server.MapPath("~/Temp/" + "username/");

            filePath = path1 + fileName;

            //打开文件
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            //读取文件的byte[]
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();

            //把byte[]转换成stream
            Stream stream = new MemoryStream(bytes);

            return File(stream, "application/octed-stream", HttpContext.Request.Browser.Browser == "IE" ? Url.Encode(fileName2) : fileName2);
        }



        /// <summary>
        /// 根据文件名称删除文件
        /// </summary>
        [HttpGet]
        public string deleteFileByFileName(string name)
        {

            string filePath = "";

            string path1 = Server.MapPath("~/Temp/" + "username/");

            filePath = path1 + name;

            fone.deleteByName(name);

            System.IO.File.Delete(filePath);

            return null;
        }


    }
}
