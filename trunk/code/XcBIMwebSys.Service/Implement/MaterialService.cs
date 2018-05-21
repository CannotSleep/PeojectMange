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
using Tmp.Data.Entity;
using XcBIMwebSys.Service.Dto;
using XcBIMwebSys.Service.Interface;
using XcBIMwebSys.Service.Tools;

namespace XcBIMwebSys.Service.Implement
{
    public partial class MaterialService:IMaterialService
    {

        IRepository<MaterialInfo> _materialRepository;
    

        public IRepository<MaterialInfo> UseRepository
        {
            get;
            set;
        }


        public MaterialService(): this(RepositoryFactory.Create<MaterialInfo>()) {

        }

        public MaterialService(IRepository<MaterialInfo> materialRepository)
        {
            _materialRepository = materialRepository;
            UseRepository = materialRepository;
        }


        public string UploadFile(HttpPostedFileBase file,string userName,int userId,string filePath,string fileName,string project,int projectid,int size,int type,string fileType,string fileExplain)
        {

            //"F:\\项目管理平台\\trunk\\code\\XcBIMwebSys.WebCode\\Content\\Temp\\username"
            //根据用户名创建文件夹
            string tPath = filePath + "\\" + fileName;
            if (System.IO.Directory.Exists(filePath) == false)//如果不存在就创建file文件夹
            {
                System.IO.Directory.CreateDirectory(filePath);
            }

            if (System.IO.File.Exists(tPath))
            {
                return "文件已存在";
            }

            string sPath = Path.Combine(filePath, fileName);
            file.SaveAs(sPath);

            var gu = Guid.NewGuid();
            string date = System.DateTime.Now.ToString();

            //文件实体类
            MaterialInfo material = new MaterialInfo();
            material.tbid = gu;
            material.tbnamebefore = file.FileName;
            material.tbnamenow = fileName;
            material.tbdate = date;
            material.tbsize = size;
            material.tbusername = userName;
            material.tbuserid = userId;
            material.tbtype = type;
            material.tbaddress = filePath;
            material.tbprojectid = projectid;
            material.tbproject = project;
            material.tbfileType = fileType;
            material.tbfileExplain = fileExplain;

            if (addMaterial(material))
            {
                return "上传成功";
            }
            else
            {

                return "上传失败";
            }

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

        //public MaterialInfo GetAccount()
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


        public bool addMaterial(MaterialInfo material)
        {
            SqlParameter[] parameters = {
                        new SqlParameter("@tb_id", SqlDbType.UniqueIdentifier) ,
                        new SqlParameter("@tb_name_before", SqlDbType.VarChar,500),
                        new SqlParameter("@tb_name_now", SqlDbType.VarChar,500),
                        new SqlParameter("@tb_date", SqlDbType.VarChar,500) ,
                        new SqlParameter("@tb_size", SqlDbType.Decimal,18) ,
                        new SqlParameter("@tb_username", SqlDbType.VarChar,500) ,
                        new SqlParameter("@tb_userid", SqlDbType.Int) ,
                        new SqlParameter("@tb_type", SqlDbType.Int) ,
                        new SqlParameter("@tb_address", SqlDbType.VarChar,500) ,
                        new SqlParameter("@tb_projectid", SqlDbType.Int) ,
                        new SqlParameter("@tb_project", SqlDbType.VarChar,500),
                        new SqlParameter("@tb_fileType", SqlDbType.VarChar,500),
                        new SqlParameter("@tb_fileExplain", SqlDbType.VarChar,500)
             };

            parameters[0].Value = material.tbid;
            parameters[1].Value = material.tbnamebefore;
            parameters[2].Value = material.tbnamenow;
            parameters[3].Value = material.tbdate;
            parameters[4].Value = material.tbsize;
            parameters[5].Value = material.tbusername;
            parameters[6].Value = material.tbuserid;
            parameters[7].Value = material.tbtype;
            parameters[8].Value = material.tbaddress;
            parameters[9].Value = material.tbprojectid;
            parameters[10].Value = material.tbproject;
            parameters[11].Value = material.tbfileType;
            parameters[12].Value = material.tbfileExplain;

            var dset= DbHelperSql.RunProcedure(DbHelperSql.DefaultUpdateConn, "MaterialInfo_ADD", parameters, "MaterialInfo");
            if (dset != null)
            {
                return true;
            }
            else {
                return false;
            }
        }

        public List<MaterialInfo> getFileList(string page,string limit, string userName, string fileName, string fileType,int jduge)
        {
            int anum = int.Parse(page);

            int bnum = int.Parse(limit);

            SqlParameter[] parameters = { };

            //string sql = "SELECT w2.n, w1.* FROM MaterialInfo w1, (  SELECT TOP " + anum * bnum + " row_number() OVER (ORDER BY tb_date asc, tb_id asc) n, tb_id FROM MaterialInfo ) w2 WHERE w1.tb_id = w2.tb_id AND w2.n > " + (anum - 1) * bnum + " ORDER BY w2.n ASC   ";
            string sqla = "SELECT w2.n, w1.* FROM MaterialInfo w1, (  SELECT TOP " + anum * bnum + " row_number() OVER (ORDER BY tb_date desc) n, tb_id FROM MaterialInfo ) w2 WHERE w1.tb_id = w2.tb_id AND w2.n > " + (anum - 1) * bnum + " AND " + " CHARINDEX('" + fileName + "', w1.tb_name_before) > 0 " + "or" + " CHARINDEX('" + userName + "', w1.tb_username) > 0 " + "or" + " CHARINDEX('" + fileType + "', w1.tb_fileType) > 0" + " ORDER BY w1.tb_date desc";
            string sqlb = "SELECT w2.n, w1.* FROM MaterialInfo w1, (  SELECT TOP " + anum * bnum + " row_number() OVER (ORDER BY tb_date desc) n, tb_id FROM MaterialInfo ) w2 WHERE w1.tb_id = w2.tb_id AND w2.n > " + (anum - 1) * bnum + " AND " + " CHARINDEX('" + fileName + "', w1.tb_name_before) > 0 " + "AND" + " CHARINDEX('" + userName + "', w1.tb_username) > 0 " + "AND" + " CHARINDEX('" + fileType + "', w1.tb_fileType) > 0" + " ORDER BY w1.tb_date desc";
            //string sql = "SELECT w2.n, w1.* FROM MaterialInfo w1, (  SELECT TOP "+ anum * bnum + " row_number() OVER (ORDER BY tb_date asc) n, tb_id FROM MaterialInfo ) w2 WHERE w1.tb_id = w2.tb_id AND w2.n > " + (anum - 1)* bnum + " ORDER BY w1.tb_date desc   ";
            //string sqla = "SELECT w2.n, w1.* FROM MaterialInfo w1, (  SELECT TOP " + anum * bnum + " row_number() OVER (ORDER BY tb_date asc, tb_id asc) n, tb_id FROM MaterialInfo ) w2 WHERE w1.tb_id = w2.tb_id AND w2.n > " + (anum - 1) * bnum + " AND " + " CHARINDEX('" + fileName + "', w1.tb_name_before) > 0 " + "or" + " CHARINDEX('" + userName + "', w1.tb_username) > 0 " + "or" + " CHARINDEX('" + fileType + "', w1.tb_fileType) > 0" + " ORDER BY w1.tb_date desc";
            //string sqlb = "SELECT w2.n, w1.* FROM MaterialInfo w1, (  SELECT TOP " + anum * bnum + " row_number() OVER (ORDER BY tb_date asc, tb_id asc) n, tb_id FROM MaterialInfo ) w2 WHERE w1.tb_id = w2.tb_id AND w2.n > " + (anum - 1) * bnum + " AND " + " CHARINDEX('" + "p" + "', w1.tb_name_before) > 0 " + "AND" + " CHARINDEX('" + userName + "', w1.tb_username) > 0 " + "AND" + " CHARINDEX('" + fileType + "', w1.tb_fileType) > 0" + " ORDER BY w1.tb_date desc";

            string sql = "";
            if (jduge == 0)
            {
                sql = sqla;
            }
            else {
                sql = sqlb;
            }


            var dataSet = DbHelperSql.Query(DbHelperSql.DefaultUpdateConn,sql);

            //var dset = DbHelperSql.RunProcedure(DbHelperSql.DefaultUpdateConn, "MaterialInfo_GetList", parameters, "MaterialInfo");

            List<MaterialInfo> list = DataTableToList(dataSet.Tables[0]);

            //list=list.OrderByDescending(a => Convert.ToDateTime(a.tbdate)).ToList();

            //list = SortAsFileCreationTimae(list);
            //Comparison<MaterialInfo> comparison = new Comparison<MaterialInfo>
            //((MaterialInfo x, MaterialInfo y) =>
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

          
            list.OrderBy(x=>Convert.ToDateTime(x.tbdate));

            return list;
        }

        //public List<MaterialInfo> getFileListSearch(string page, string limit, string user, string fileName, string fileType)
        //{
        //    int anum = int.Parse(page);

        //    int bnum = int.Parse(limit);

        //    SqlParameter[] parameters = { };

        //    string sqla = "SELECT w2.n, w1.* FROM MaterialInfo w1, (  SELECT TOP " + anum * bnum + " row_number() OVER (ORDER BY tb_date asc, tb_id asc) n, tb_id FROM MaterialInfo ) w2 WHERE w1.tb_id = w2.tb_id AND w2.n > " + (anum - 1) * bnum +" AND "+ " CHARINDEX('" + fileName + "', w1.tb_name_before) > 0 " + "or" + " CHARINDEX('" + user + "', w1.tb_username) > 0 " + "or" + "CHARINDEX('" + fileType + "', w1.tb_fileType) > 0" + " ORDER BY w2.n ASC  ";
        //    string sqlb = "SELECT w2.n, w1.* FROM MaterialInfo w1, (  SELECT TOP " + anum * bnum + " row_number() OVER (ORDER BY tb_date asc, tb_id asc) n, tb_id FROM MaterialInfo ) w2 WHERE w1.tb_id = w2.tb_id AND w2.n > " + (anum - 1) * bnum +" AND " + " CHARINDEX('" + fileName + "', w1.tb_name_before) > 0 " + "AND" + " CHARINDEX('" + user + "', w1.tb_username) > 0 " + "AND" + "CHARINDEX('" + fileType + "', w1.tb_fileType) > 0" + " ORDER BY w2.n ASC  ";
        //    string sql = "";

        //    //string sql = "SELECT w2.n, w1.* FROM MaterialInfo w1, (  SELECT TOP "+ anum * bnum + " row_number() OVER (ORDER BY tb_date asc) n, tb_id FROM MaterialInfo ) w2 WHERE w1.tb_id = w2.tb_id AND w2.n > " + (anum - 1)* bnum + " ORDER BY w1.tb_date desc   ";

        //    var dataSet = DbHelperSql.Query(DbHelperSql.DefaultUpdateConn, sql);

        //    //var dset = DbHelperSql.RunProcedure(DbHelperSql.DefaultUpdateConn, "MaterialInfo_GetList", parameters, "MaterialInfo");

        //    List<MaterialInfo> list = DataTableToList(dataSet.Tables[0]);

        //    list.OrderBy(x => x.tbdate);

        //    return list;
        //}



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<MaterialInfo> DataTableToList(DataTable dt)
        {
            List<MaterialInfo> modelList = new List<MaterialInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                MaterialInfo model;
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
        public MaterialInfo DataRowToModel(DataRow row)
        {
            MaterialInfo model = new MaterialInfo();
            if (row != null)
            {
                if (row["tb_id"] != null && row["tb_id"].ToString() != "")
                {
                    model.tbid = new Guid(row["tb_id"].ToString());
                }

                if (row["tb_name_before"] != null && row["tb_name_before"].ToString() != "")
                {
                    model.tbnamebefore = row["tb_name_before"].ToString();
                }

                if (row["tb_name_now"] != null && row["tb_name_now"].ToString() != "")
                {
                    model.tbnamenow = row["tb_name_now"].ToString();
                }

                if (row["tb_date"] != null && row["tb_date"].ToString() != "")
                {
                    model.tbdate = row["tb_date"].ToString();
                }

                if (row["tb_size"] != null && row["tb_size"].ToString() != "")
                {
                    model.tbsize = int.Parse(row["tb_size"].ToString());
                }

                if (row["tb_username"] != null && row["tb_username"].ToString() != "")
                {
                    model.tbusername = row["tb_username"].ToString();
                }

                if (row["tb_userid"] != null && row["tb_userid"].ToString() != "")
                {
                    model.tbuserid = int.Parse(row["tb_userid"].ToString());
                }

                if (row["tb_type"] != null && row["tb_type"].ToString() != "")
                {
                    model.tbtype = int.Parse(row["tb_type"].ToString());
                }

                if (row["tb_address"] != null && row["tb_address"].ToString() != "")
                {
                    model.tbaddress = row["tb_address"].ToString();
                }

                if (row["tb_projectid"] != null && row["tb_projectid"].ToString() != "")
                {
                    model.tbprojectid = int.Parse(row["tb_projectid"].ToString());
                }

                if (row["tb_project"] != null && row["tb_project"].ToString() != "")
                {
                    model.tbproject = row["tb_project"].ToString();
                }


                if (row["tb_fileType"] != null && row["tb_fileType"].ToString() != "")
                {
                    model.tbfileType = row["tb_fileType"].ToString();
                }
                if (row["tb_fileExplain"] != null && row["tb_fileExplain"].ToString() != "")
                {
                    model.tbfileExplain = row["tb_fileExplain"].ToString();
                }
            }
            return model;
        }

        public bool deleteFile(string path,string name)
        {

            System.IO.File.Delete(path);

            SqlParameter[] parameters = {
                        new SqlParameter("@tb_name_before", SqlDbType.VarChar,500)
             };

            parameters[0].Value = name;
         

            var dset = DbHelperSql.RunProcedure(DbHelperSql.DefaultUpdateConn, "MaterialInfo_Delete", parameters, "MaterialInfo");
            if (dset != null)
            {
                return true;
            }
            else
            {
                return false;
            }
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

        public string getTotal(string tableName, string userName, string fileName, string fileType,int jduge)
        {
            //string sql = "select count(*) from " + tableName + " where " + " CHARINDEX('" + fileName + "',tb_name_before)>0";

            string sqla = "select count(*) from " + tableName + " where CHARINDEX('" + fileName + "',tb_name_before)>0 or " + "CHARINDEX('" + userName + "',tb_username)>0 or " + "CHARINDEX('" + fileType + "',tb_fileType)>0";

            string sqlb = "select count(*) from " + tableName + " where CHARINDEX('" + fileName + "',tb_name_before)>0 AND "+ "CHARINDEX('" + userName + "',tb_username)>0 AND "+ "CHARINDEX('" + fileType + "',tb_fileType)>0";

            string sql = "";

            if (jduge == 0)
            {
                sql = sqla;
            }
            else {
                sql = sqlb;
            }

            var dataCount = DbHelperSql.GetSingle(DbHelperSql.DefaultUpdateConn, sql);

            return dataCount + "";
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


