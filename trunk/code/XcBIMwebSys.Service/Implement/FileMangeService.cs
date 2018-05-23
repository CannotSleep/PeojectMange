using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tmp.Core.Data;
using Tmp.Data;
using Tmp.Service;
using Tmp.Data.Entity;
using XcBIMwebSys.Service.Dto;
using XcBIMwebSys.Service.Interface;
using XcBIMwebSys.Service.Tools;
using System.Linq.Expressions;

namespace XcBIMwebSys.Service.Implement
{
    public partial class FileMangeService:IFileMangeService
    {

        IRepository<FileMange> _materialRepository;
        IRepository<FileModular> _materialRepository_fm;
        

        FileMangeTool fileMangeTool = new FileMangeTool();

        public IRepository<FileMange> UseRepository
        {
            get;
            set;
        }

        public IRepository<FileModular> UseRepository_FM
        {
            get;
            set;
        }

        public FileMangeService(): this(RepositoryFactory.Create<FileMange>(), RepositoryFactory.Create<FileModular>()) {

        }


        public FileMangeService(IRepository<FileMange> materialRepository, IRepository<FileModular> materialRepositor_fm)
        {
            _materialRepository = materialRepository;
            _materialRepository_fm = materialRepositor_fm;
            UseRepository = materialRepository;
            UseRepository_FM = materialRepositor_fm;
        }

        /// <summary>upload file</summary>
        /// <param name="file">前端上传文件</param>
        /// <param name="NameBefore">文件原名称</param>
        /// <param name="Size">文件大小</param>
        /// <param name="UserId">用户id</param>
        /// <param name="FileType">文件后缀类型</param>
        /// <param name="FilePath">文件路径</param>
        /// <param name="PeojectId">项目id</param>
        /// <param name="FileCategory">文件分类</param>
        /// <param name="FileExplain">文件说明</param>
        public string UploadFile(HttpPostedFileBase file,string NameBefore, int Size, Guid UserId, string FileType, string FilePath, Guid PeojectId, string FileCategory,string FileExplain,string UserName,string ProjectName)
        {

            //"F:\\项目管理平台\\trunk\\code\\XcBIMwebSys.WebCode\\Content\\Temp\\username"
            //根据用户名创建文件夹
            string tPath = FilePath + "\\" + NameBefore;
            if (System.IO.Directory.Exists(FilePath) == false)//如果不存在就创建file文件夹
            {
                System.IO.Directory.CreateDirectory(FilePath);
            }

            if (System.IO.File.Exists(tPath))
            {
                return "文件已存在";
            }

            var gu = Guid.NewGuid();

            //文件名修改
            string NameNow = NameBefore;

            string sPath = Path.Combine(FilePath, NameNow);

            file.SaveAs(sPath);

            string date = System.DateTime.Now.ToString();

            //文件路径加密
            FilePath = fileMangeTool.DESEncrypt(FilePath+"\\"+NameNow,"12345678","87654321");


            //文件实体类
            FileMange material = new FileMange();
            material.FileID = gu;
            material.NameBefore = NameBefore;
            material.NameNow = NameNow;
            material.Date = date;
            material.Size = Size;
            material.UserId = UserId;
            material.FileType = FileType;
            material.FilePath = FilePath;
            material.ProjectId = PeojectId;
            material.FileCategory = FileCategory;
            material.FileExplain = FileExplain;
            material.UserName = UserName;
            material.ProjectName = ProjectName;

            FileModular fileModular = new FileModular();
            var fmu = Guid.NewGuid();
            fileModular.FileModularID = fmu;
            fileModular.FileType = FileCategory;
            fileModular.RefFIleID = gu;
            fileModular.RefProjectID = PeojectId;

            try
            {
                _materialRepository.Insert(material);
                _materialRepository_fm.Insert(fileModular);
            }
            catch (Exception e)
            {
                return "上传失败";
            }



            //if (addMaterial(material))
            //{
            //    return "上传成功";
            //}
            //else
            //{

            //    return "上传失败";
            //}
            return "上传成功";
        }


        //public List<MaterialDto> GteAccountList(MaterialDto model)
        //{
        //    var query = from a in _materialRepository.Table
        //                select new MaterialDto
        //                {
        //                    tb_id = a.tbid,
        //                    tb_name_before = a.tbnamebefore,
        //                    tb_name_now = a.tbnamenow,
        //                    tb_date = a.tbdate,
        //                    tb_size = a.tbsize,
        //                    tb_username = a.tbusername,
        //                    tb_userid = a.tbuserid,
        //                    tb_type = a.tbtype,
        //                    tb_address = a.tbaddress,
        //                    tb_projectid = a.tbprojectid,
        //                    tb_project = a.tbproject
        //                };
        //    if (model.tb_id != null && model.tb_id != Guid.Empty)
        //        query = query.Where(t => t.tb_id == model.tb_id);
        //    //if (!string.IsNullOrWhiteSpace(model.tb_name_before))
        //    //    query = query.Where(t => t.tb_name_before == model.tb_name_before);
        //    //if (!string.IsNullOrWhiteSpace(model.tb_name_now))
        //    //    query = query.Where(t => t.tb_name_now == model.tb_name_now);
        //    //if (model.RolesID != 0)
        //    //    query = query.Where(t => t.RolesID == model.RolesID);
        //    //if (model.ID != null && model.ID != Guid.Empty)
        //    //    query = query.Where(t => t.ID == model.ID);
        //    //if (!string.IsNullOrWhiteSpace(model.AccountID))
        //    //    query = query.Where(t => t.AccountID == model.AccountID);
        //    //if (model.DepartmentsID != 0)
        //    //    query = query.Where(t => t.DepartmentsID == model.DepartmentsID);
        //    //if (model.RolesID != 0)
        //    //    query = query.Where(t => t.RolesID == model.RolesID);
        //    //if (model.ID != null && model.ID != Guid.Empty)
        //    //    query = query.Where(t => t.ID == model.ID);
        //    //if (!string.IsNullOrWhiteSpace(model.AccountID))
        //    //    query = query.Where(t => t.AccountID == model.AccountID);
        //    //if (model.DepartmentsID != 0)
        //    //    query = query.Where(t => t.DepartmentsID == model.DepartmentsID);
        //    return query.ToList();
        //}

        //public FileMange GetAccount()
        //{
        //    //方法一：通过主键获得
        //    var acconnt = _materialRepository.GetById(new Guid("2EA2D746-C57B-47F7-BA74-9E8D73D8D7C1"));
        //    //方法二：通过参数ID查询
        //    //var acconnt2 = _accountRepository.Find("ID=@ID", new SqlParameter[] {
        //    //    new SqlParameter("@ID",new Guid("2EA2D746-C57B-47F7-BA74-9E8D73D8D7C1"))
        //    //});
        //    //方法三：通过linq查询
        //    var query = from u in _materialRepository.Table
        //                select u;
        //    //query = query.Where(t => t.AccountID == "admin");
        //    return query.First();
        //}

        //public bool BatchAdd(DataTable dt)
        //{
        //    SqlParameter[] parameters = {
        //        new SqlParameter("@ErrorNo", SqlDbType.Int, 4),
        //        new SqlParameter("@ErrorMsg", SqlDbType.NVarChar, 500),
        //        new SqlParameter("@tpImportAccount", dt)
        //    };
        //    parameters[0].Direction = ParameterDirection.Output;
        //    parameters[1].Direction = ParameterDirection.Output;

        //    DbHelperSql.RunProcedure(DbHelperSql.DefaultUpdateConn, "sp_BatchImportAccount", parameters, "ds");
        //    if ((int)parameters[0].Value == 1)
        //    {
        //        return true;
        //    }
        //    else
        //        return false;
        //}


        public bool addMaterial(FileMange material)
        {
            #region
            //SqlParameter[] parameters = {
            //            new SqlParameter("@tb_id", SqlDbType.UniqueIdentifier) ,
            //            new SqlParameter("@tb_name_before", SqlDbType.VarChar,500),
            //            new SqlParameter("@tb_name_now", SqlDbType.VarChar,500),
            //            new SqlParameter("@tb_date", SqlDbType.VarChar,500) ,
            //            new SqlParameter("@tb_size", SqlDbType.Decimal,18) ,
            //            new SqlParameter("@tb_username", SqlDbType.VarChar,500) ,
            //            new SqlParameter("@tb_userid", SqlDbType.Int) ,
            //            new SqlParameter("@tb_type", SqlDbType.Int) ,
            //            new SqlParameter("@tb_address", SqlDbType.VarChar,500) ,
            //            new SqlParameter("@tb_projectid", SqlDbType.Int) ,
            //            new SqlParameter("@tb_project", SqlDbType.VarChar,500),
            //            new SqlParameter("@tb_fileType", SqlDbType.VarChar,500),
            //            new SqlParameter("@tb_fileExplain", SqlDbType.VarChar,500)
            // };

            //parameters[0].Value = material.tbid;
            //parameters[1].Value = material.tbnamebefore;
            //parameters[2].Value = material.tbnamenow;
            //parameters[3].Value = material.tbdate;
            //parameters[4].Value = material.tbsize;
            //parameters[5].Value = material.tbusername;
            //parameters[6].Value = material.tbuserid;
            //parameters[7].Value = material.tbtype;
            //parameters[8].Value = material.tbaddress;
            //parameters[9].Value = material.tbprojectid;
            //parameters[10].Value = material.tbproject;
            //parameters[11].Value = material.tbfileType;
            //parameters[12].Value = material.tbfileExplain;


            //var dset= DbHelperSql.RunProcedure(DbHelperSql.DefaultUpdateConn, "FileMange_ADD", parameters, "FileMange");
            //if (dset != null)
            //{
            //    return true;
            //}
            //else {
            //    return false;
            //}
            #endregion


            //StringBuilder strSql = new StringBuilder();

            //strSql.Append("");

            //DbHelperSql.ExecuteSql(DbHelperSql.DefaultUpdateConn, strSql.ToString());




            return false;
        }


        /// <summary>upload file</summary>
        /// <param name="file">前端上传文件</param>
        /// <param name="NameBefore">文件原名称</param>
        /// <param name="Size">文件大小</param>
        /// <param name="UserId">用户id</param>
        /// <param name="FileType">文件后缀类型</param>
        public List<FileMange> getFileList(string page,string limit,string fileName, string fileType,string userName)
        {
            int anum = int.Parse(page);

            int bnum = int.Parse(limit);

            Expression<Func<FileMange, bool>> where = (m => m.FileID != null);
            #region
            //switch (jduge){
            //    case 1:
            //        where = m => m.NameBefore.Contains(fileName);
            //         break;
            //    case 2:
            //        m.FileCategory.Contains(fileType)
            //        break;
            //    case 3:
            //        m.UserName.Contains(userName)
            //        break;
            //    case 4:

            //        break;
            //    default:
            //        break;
            //}
            #endregion

            List<FileMange> list = _materialRepository.GetFileList(where, m => m.Date, anum, bnum);
            #region
            //SqlParameter[] parameters = { };

            //string sql = "SELECT w2.n, w1.* FROM FileMange w1, (  SELECT TOP " + anum * bnum + " row_number() OVER (ORDER BY tb_date asc, tb_id asc) n, tb_id FROM FileMange ) w2 WHERE w1.tb_id = w2.tb_id AND w2.n > " + (anum - 1) * bnum + " ORDER BY w2.n ASC   ";
            //string sqla = "SELECT w2.n, w1.* FROM FileMange w1, (  SELECT TOP " + anum * bnum + " row_number() OVER (ORDER BY tb_date desc) n, tb_id FROM FileMange ) w2 WHERE w1.tb_id = w2.tb_id AND w2.n > " + (anum - 1) * bnum + " AND " + " CHARINDEX('" + fileName + "', w1.tb_name_before) > 0 " + "or" + " CHARINDEX('" + fileType + "', w1.tb_fileType) > 0" + " ORDER BY w1.tb_date desc";
            //string sqlb = "SELECT w2.n, w1.* FROM FileMange w1, (  SELECT TOP " + anum * bnum + " row_number() OVER (ORDER BY tb_date desc) n, tb_id FROM FileMange ) w2 WHERE w1.tb_id = w2.tb_id AND w2.n > " + (anum - 1) * bnum + " AND " + " CHARINDEX('" + fileName + "', w1.tb_name_before) > 0 " + "AND" + " CHARINDEX('" + fileType + "', w1.tb_fileType) > 0" + " ORDER BY w1.tb_date desc";
            //string sql = "SELECT w2.n, w1.* FROM FileMange w1, (  SELECT TOP "+ anum * bnum + " row_number() OVER (ORDER BY tb_date asc) n, tb_id FROM FileMange ) w2 WHERE w1.tb_id = w2.tb_id AND w2.n > " + (anum - 1)* bnum + " ORDER BY w1.tb_date desc   ";
            //string sqla = "SELECT w2.n, w1.* FROM FileMange w1, (  SELECT TOP " + anum * bnum + " row_number() OVER (ORDER BY tb_date asc, tb_id asc) n, tb_id FROM FileMange ) w2 WHERE w1.tb_id = w2.tb_id AND w2.n > " + (anum - 1) * bnum + " AND " + " CHARINDEX('" + fileName + "', w1.tb_name_before) > 0 " + "or" + " CHARINDEX('" + userName + "', w1.tb_username) > 0 " + "or" + " CHARINDEX('" + fileType + "', w1.tb_fileType) > 0" + " ORDER BY w1.tb_date desc";
            //string sqlb = "SELECT w2.n, w1.* FROM FileMange w1, (  SELECT TOP " + anum * bnum + " row_number() OVER (ORDER BY tb_date asc, tb_id asc) n, tb_id FROM FileMange ) w2 WHERE w1.tb_id = w2.tb_id AND w2.n > " + (anum - 1) * bnum + " AND " + " CHARINDEX('" + "p" + "', w1.tb_name_before) > 0 " + "AND" + " CHARINDEX('" + userName + "', w1.tb_username) > 0 " + "AND" + " CHARINDEX('" + fileType + "', w1.tb_fileType) > 0" + " ORDER BY w1.tb_date desc";

            //string sql = "";
            //if (jduge == 0)
            //{
            //    sql = sqla;
            //}
            //else
            //{
            //    sql = sqlb;
            //}
            #endregion
            //待改进 条件叠加
            if (userName != null && userName != "")
            {
                where = (m => m.UserName.Contains(userName));
                list= _materialRepository.GetFileList(where, m => m.Date, anum, bnum);
            }
            if (fileName != null && fileName != "" )
            {
                where = (m => m.NameBefore.Contains(fileName));
                list = _materialRepository.GetFileList(where, m => m.Date, anum, bnum);
            }
            if (fileType != null && fileType != "" )
            {
                where = (m =>m.FileCategory.Contains(fileType));
                list = _materialRepository.GetFileList(where, m => m.Date, anum, bnum);
            }

            foreach (FileMange fm in list) {
                fm.FilePath = fileMangeTool.PathEncryption(fm.FilePath,"12345678", "87654321");
            }

            #region
            //var dataSet = DbHelperSql.Query(DbHelperSql.DefaultUpdateConn,sql);

            //var dset = DbHelperSql.RunProcedure(DbHelperSql.DefaultUpdateConn, "FileMange_GetList", parameters, "FileMange");

            //List<FileMange> list = DataTableToList(dataSet.Tables[0]);

            //list=list.OrderByDescending(a => Convert.ToDateTime(a.tbdate)).ToList();

            //list = SortAsFileCreationTimae(list);
            //Comparison<FileMange> comparison = new Comparison<FileMange>
            //((FileMange x, FileMange y) =>
            //{
            //    DateTime date1 = Convert.ToDateTime(x.tbdate);
            //    DateTime date2 = Convert.ToDateTime(y.tbdate);

            //    if (DateTime.Compare(date1, date2) < 0)
            //        return 1;
            //    else if (DateTime.Compare(date1, date2) == 0)
            //        return 0;
            //    else
            //        return -1;
            //});


            //list.OrderBy(x=>Convert.ToDateTime(x.Date));
            #endregion
            return list;
        }

        #region
        //public List<FileMange> getFileListSearch(string page, string limit, string user, string fileName, string fileType)
        //{
        //    int anum = int.Parse(page);

        //    int bnum = int.Parse(limit);

        //    SqlParameter[] parameters = { };

        //    string sqla = "SELECT w2.n, w1.* FROM FileMange w1, (  SELECT TOP " + anum * bnum + " row_number() OVER (ORDER BY tb_date asc, tb_id asc) n, tb_id FROM FileMange ) w2 WHERE w1.tb_id = w2.tb_id AND w2.n > " + (anum - 1) * bnum +" AND "+ " CHARINDEX('" + fileName + "', w1.tb_name_before) > 0 " + "or" + " CHARINDEX('" + user + "', w1.tb_username) > 0 " + "or" + "CHARINDEX('" + fileType + "', w1.tb_fileType) > 0" + " ORDER BY w2.n ASC  ";
        //    string sqlb = "SELECT w2.n, w1.* FROM FileMange w1, (  SELECT TOP " + anum * bnum + " row_number() OVER (ORDER BY tb_date asc, tb_id asc) n, tb_id FROM FileMange ) w2 WHERE w1.tb_id = w2.tb_id AND w2.n > " + (anum - 1) * bnum +" AND " + " CHARINDEX('" + fileName + "', w1.tb_name_before) > 0 " + "AND" + " CHARINDEX('" + user + "', w1.tb_username) > 0 " + "AND" + "CHARINDEX('" + fileType + "', w1.tb_fileType) > 0" + " ORDER BY w2.n ASC  ";
        //    string sql = "";

        //    //string sql = "SELECT w2.n, w1.* FROM FileMange w1, (  SELECT TOP "+ anum * bnum + " row_number() OVER (ORDER BY tb_date asc) n, tb_id FROM FileMange ) w2 WHERE w1.tb_id = w2.tb_id AND w2.n > " + (anum - 1)* bnum + " ORDER BY w1.tb_date desc   ";

        //    var dataSet = DbHelperSql.Query(DbHelperSql.DefaultUpdateConn, sql);

        //    //var dset = DbHelperSql.RunProcedure(DbHelperSql.DefaultUpdateConn, "FileMange_GetList", parameters, "FileMange");

        //    List<FileMange> list = DataTableToList(dataSet.Tables[0]);

        //    list.OrderBy(x => x.tbdate);

        //    return list;
        //}
        #endregion


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<FileMange> DataTableToList(DataTable dt)
        {
            List<FileMange> modelList = new List<FileMange>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                FileMange model;
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
        public FileMange DataRowToModel(DataRow row)
        {
            FileMange model = new FileMange();
            if (row != null)
            {
                if (row["FileID"] != null && row["FileID"].ToString() != "")
                {
                    model.FileID = new Guid(row["FileID"].ToString());
                }

                if (row["NameBefore"] != null && row["NameBefore"].ToString() != "")
                {
                    model.NameBefore = row["NameBefore"].ToString();
                }

                if (row["NameNow"] != null && row["NameNow"].ToString() != "")
                {
                    model.NameNow = row["NameNow"].ToString();
                }

                if (row["Date"] != null && row["Date"].ToString() != "")
                {
                    model.Date = row["Date"].ToString();
                }

                if (row["Size"] != null && row["Size"].ToString() != "")
                {
                    model.Size = int.Parse(row["Size"].ToString());
                }

                if (row["UserId"] != null && row["UserId"].ToString() != "")
                {
                    model.UserId =new Guid(row["UserId"].ToString());
                }

                if (row["FileType"] != null && row["FileType"].ToString() != "")
                {
                    model.FileType = row["FileType"].ToString();
                }

                if (row["FilePath"] != null && row["FilePath"].ToString() != "")
                {
                    model.FilePath = row["FilePath"].ToString();
                }

                if (row["ProjectId"] != null && row["ProjectId"].ToString() != "")
                {
                   model.ProjectId = new Guid(row["ProjectId"].ToString());
                }    

                if (row["FileCategory"] != null && row["FileCategory"].ToString() != "")
                {
                    model.FileCategory = row["FileCategory"].ToString();
                }
                if (row["FileExplain"] != null && row["FileExplain"].ToString() != "")
                {
                    model.FileExplain = row["FileExplain"].ToString();
                }
                if (row["UserName"] != null && row["UserName"].ToString() != "")
                {
                    model.UserName = row["UserName"].ToString();
                }
                if (row["ProjectName"] != null && row["ProjectName"].ToString() != "")
                {
                    model.ProjectName = row["ProjectName"].ToString();
                }
            }
            return model;
        }

        public bool deleteFile(string path,string id)
        {
            try {
                Guid gu = new Guid(id);

                

                FileMange fm = _materialRepository.GetById(gu);


                System.IO.File.Delete(path + fm.NameBefore);

                List<FileModular> list = _materialRepository_fm.GetFileModular( m=>m.RefFIleID==gu, m => m.FileType);

                foreach (FileModular fmu in list) {
                _materialRepository_fm.Delete(fmu);
                }

                _materialRepository.Delete(fm);
                     return true;
                } catch (Exception ex){
                    return false;
                }
                
            //SqlParameter[] parameters = {
            //            new SqlParameter("@tb_name_before", SqlDbType.VarChar,500)
            // };

            //parameters[0].Value = name;
         

            //var dset = DbHelperSql.RunProcedure(DbHelperSql.DefaultUpdateConn, "FileMange_Delete", parameters, "FileMange");
            //if (dset != null)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        public Stream downFile(string path)
        {

            //打开文件
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            //读取文件的byte[]
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();

            //把byte[]转换成stream
            Stream stream = new MemoryStream(bytes);

            return stream;
        }

        public string getTotal(string tableName,  string fileName, string fileType,string userName)
        {
            //string sql = "select count(*) from " + tableName + " where " + " CHARINDEX('" + fileName + "',tb_name_before)>0";

            //string sqla = "select count(*) from " + tableName + " where CHARINDEX('" + fileName + "',tb_name_before)>0 or " +  "CHARINDEX('" + fileType + "',tb_fileType)>0";

            //string sqlb = "select count(*) from " + tableName + " where CHARINDEX('" + fileName + "',tb_name_before)>0 AND "+  "CHARINDEX('" + fileType + "',tb_fileType)>0";

            //string sql = "";

            //if (jduge == 0)
            //{
            //    sql = sqla;
            //}
            //else {
            //    sql = sqlb;
            //}

            //sql = "select count(*) from FileMange";

            //var dataCount = DbHelperSql.GetSingle(DbHelperSql.DefaultUpdateConn, sql);

            Expression<Func<FileMange, bool>> where = (m => m.FileID != null);

            int count = _materialRepository.GetDataTotal(where);        

            //待改进 条件叠加
            if (userName != null && userName != "")
            {
                where = (m => m.UserName.Contains(userName));
                count = _materialRepository.GetDataTotal(where);
            }
            if (fileName != null && fileName != "")
            {
                where = (m => m.NameBefore.Contains(fileName));
                count = _materialRepository.GetDataTotal(where);
            }
            if (fileType != null && fileType != "")
            {
                where = (m => m.FileCategory.Contains(fileType));
                count = _materialRepository.GetDataTotal(where);
            }

           

            return count + "";
        }

        public string readExcel(string exePath)
        {

            ExcelToDS("");
            return null;
        }


        public DataSet ExcelToDS(string Path)
        {
            Path = "F:\\项目管理平台\\trunk\\code\\XcBIMwebSys.WebCode\\Temp\\123654\\test.xlsx";
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 12.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "select * from [sheet1$]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");
            foreach (DataRow col in ds.Tables[0].Rows)           //set.Tables[0].Rows 找到指定表的所有行 0这里可以填表名
            {
                Console.WriteLine(col[0].ToString());                 //col[0]这一行的索引是0单元格，也就是列，你只要在0这里填上你要输出的第几列就行了
            }
            return ds;
        }

        //public string getTotalSearch(string tableName,string userName,string fileName,string fileType)
        //{
        //    string sql = "select count(*) from " + tableName+"where"+ " CHARINDEX('" + fileName + "',tb_name_before)>0"+"AND"+ " CHARINDEX('" + userName + "',tb_username)>0" + "AND"+ " CHARINDEX('" + fileType + "',tb_fileType)>0";

        //    var dataCount = DbHelperSql.GetSingle(DbHelperSql.DefaultUpdateConn, sql);

        //    return dataCount + "";
        //}
    }

    
}


