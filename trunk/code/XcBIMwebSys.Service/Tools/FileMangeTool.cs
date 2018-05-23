using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace XcBIMwebSys.Service.Tools
{
    public class FileMangeTool
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
        public string getFileType(string fileName)
        {
            string c = fileName.Substring(fileName.LastIndexOf("."));
            //int type = 0;
            //switch (c)
            //{
            //    case ".jpg":
            //        type = 0;
            //        break;
            //    case ".txt":
            //        type = 1;
            //        break;
            //    case ".doc":
            //        type = 1;
            //        break;
            //    case ".png":
            //        type = 0;
            //        break;
            //    case ".docx":
            //        type = 1;
            //        break;
            //    default:
            //        type = 1;
            //        break;
            //}
            return c.Substring(1);
        }

        //文件类型转换，，，用于文件预览

        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="filePath">文件实际路径</param>
        /// <returns></returns>
        public string MD5Encrypt16(string filePath)
        {
            var md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(filePath)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }



        /// <summary>
        /// DES加密解密
        /// </summary>
        /// <param name="filePath">文件实际路径</param>
        /// <returns></returns>
        public string PathEncryption(string filePath)
        {
            DESCryptoServiceProvider DesCSP = new DESCryptoServiceProvider();
            byte[] buffer;
            MemoryStream ms = new MemoryStream();//先创建 一个内存流
            CryptoStream cryStream = new CryptoStream(ms, DesCSP.CreateEncryptor(), CryptoStreamMode.Write);//将内存流连接到加密转换流
            StreamWriter sw = new StreamWriter(cryStream);
            sw.WriteLine(filePath);//将要加密的字符串写入加密转换流
            sw.Close();
            cryStream.Close();
            buffer = ms.ToArray();//将加密后的流转换为字节数组
            return Convert.ToBase64String(buffer);//将加密后的字节数组转换为字符串
        }


        /// <summary>
        /// DES加密解密
        /// </summary>
        /// <param name="filePath">文件实际路径</param>
        /// <returns></returns>
        public string PathCryption(string filePath)
        {
            DESCryptoServiceProvider DesCSP = new DESCryptoServiceProvider();
            byte[] buffer= Convert.FromBase64String(filePath); ;
            MemoryStream ms = new MemoryStream(buffer);//将加密后的字节数据加入内存流中
            CryptoStream cryStream = new CryptoStream(ms, DesCSP.CreateDecryptor(), CryptoStreamMode.Read);//内存流连接到解密流中
            StreamReader sr = new StreamReader(cryStream);
            string path= sr.ReadLine();//将解密流读取为字符串
            sr.Close();
            cryStream.Close();
            ms.Close();
            return path;
        }


    }
}