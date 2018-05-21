using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Tmp.Core.Dependency;
using Tmp.Data.Entity;
using Tmp.Service;
using XcBIMwebSys.Service.Dto;

namespace XcBIMwebSys.Service.Interface
{
    public interface IMaterialService : IService<MaterialInfo>
    {
       

        string UploadFile(HttpPostedFileBase file,string userName,int userid,string filePath,string fileName,string project,int projectid,int size,int type,string fileType,string fileExplain);

        List<MaterialInfo> getFileList(string page,string limit,string userName,string fileName,string fileType,int jduge);

        Boolean deleteFile(string path,string name);

        Stream downFile(string name);

        string getTotal(string tableName, string userName, string fileName, string fileType,int jduge);

        string readExcel(string exePath);

        //string getTotalSearch(string tableName,string userName,string fileName,string fileType);

        //List<MaterialInfo> getFileListSearch(string page, string limit,string user,string fileName,string fileType);

    }
}
