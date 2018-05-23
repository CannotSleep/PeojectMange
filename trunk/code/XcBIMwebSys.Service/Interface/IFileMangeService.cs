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
    public interface IFileMangeService : IService<FileMange>
    {

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
        string UploadFile(HttpPostedFileBase file,string NameBefore,int Size,Guid UserId,string FileType,string FilePath,Guid PeojectId,string FileCategory,string FileExplain,string UserName,string ProjectName);

        List<FileMange> getFileList(string page,string limit,string fileName,string fileType, string userName);

        Boolean deleteFile(string path,string name);

        Stream downFile(string name);

        string getTotal(string tableName,string fileName, string fileType, string userName);

        string readExcel(string exePath);

        //string getTotalSearch(string tableName,string userName,string fileName,string fileType);

        //List<MaterialInfo> getFileListSearch(string page, string limit,string user,string fileName,string fileType);

    }
}
