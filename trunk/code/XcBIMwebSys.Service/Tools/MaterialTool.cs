using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace XcBIMwebSys.Service.Tools
{
    public class MaterialTool
    {

        /// <summary>  
        /// 获取指定目录大小  
        /// </summary>  
        /// <param name="path">目录物理路径</param>  
        /// <param name="unitType">返回单位：Byte/KB/MB/GB</param>  
        /// <returns></returns>  
        public decimal getDirectorySize(string path, string unitType = "Byte")
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            decimal sumSize = 0;
            foreach (FileSystemInfo fsInfo in dirInfo.GetFileSystemInfos())
            {
                if (fsInfo.Attributes.ToString().ToLower().Contains("directory"))
                {
                    if (!fsInfo.Attributes.ToString().ToLower().Contains("system"))
                    {
                        sumSize += getDirectorySize(fsInfo.FullName);
                    }
                }
                else
                {
                    FileInfo fiInfo = new FileInfo(fsInfo.FullName);
                    sumSize += fiInfo.Length;
                }
            }

            if (unitType == "KB")
            {
                sumSize = sumSize / 1024;
            }
            else if (unitType == "MB")
            {
                sumSize = sumSize / 1024 / 1024;
            }
            else if (unitType == "GB")
            {
                sumSize = sumSize / 1024 / 1024 / 1024;
            }
            return sumSize;
        }

        //获取日期方法
        public string getNowDate()
        {
            return DateTime.Now.ToLocalTime().ToString();
        }

        //获取文件类型
        public int getFileType(string fileName)
        {
            string c = fileName.Substring(fileName.LastIndexOf("."));
            int type = 0;
            switch (c)
            {
                case ".jpg":
                    type = 0;
                    break;
                case ".txt":
                    type = 1;
                    break;
                case ".doc":
                    type = 1;
                    break;
                case ".png":
                    type = 0;
                    break;
                case ".docx":
                    type = 1;
                    break;
                default:
                    type = 1;
                    break;
            }
            return type;
        }

        //文件类型转换，，，用于文件预览





    }
}