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
        /// C# DES解密方法  
        /// </summary>  
        /// <param name="encryptedValue">待解密的字符串</param>  
        /// <param name="key">密钥</param>  
        /// <param name="iv">向量</param>  
        /// <returns>解密后的字符串</returns>  
        public string PathEncryption(string encryptedValue, string key, string iv)
        {
            using (DESCryptoServiceProvider sa =
                     new DESCryptoServiceProvider
                { Key = Encoding.UTF8.GetBytes(key), IV = Encoding.UTF8.GetBytes(iv) })
            {
                using (ICryptoTransform ct = sa.CreateDecryptor())
                {
                    byte[] byt = Convert.FromBase64String(encryptedValue);

                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, ct, CryptoStreamMode.Write))
                        {
                            cs.Write(byt, 0, byt.Length);
                            cs.FlushFinalBlock();
                        }
                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
        }


        /// <summary>  
        /// C# DES加密方法  
        /// </summary>  
        /// <param name="encryptedValue">要加密的字符串</param>  
        /// <param name="key">密钥</param>  
        /// <param name="iv">向量</param>  
        /// <returns>加密后的字符串</returns>  
        public  string DESEncrypt(string originalValue, string key, string iv)
        {
            using (DESCryptoServiceProvider sa
                = new DESCryptoServiceProvider { Key = Encoding.UTF8.GetBytes(key), IV = Encoding.UTF8.GetBytes(iv) })
            {
                using (ICryptoTransform ct = sa.CreateEncryptor())
                {
                    byte[] by = Encoding.UTF8.GetBytes(originalValue);
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, ct,
                                                         CryptoStreamMode.Write))
                        {
                            cs.Write(by, 0, by.Length);
                            cs.FlushFinalBlock();
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }


    }
}