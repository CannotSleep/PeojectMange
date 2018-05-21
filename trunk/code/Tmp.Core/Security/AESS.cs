using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Tmp.Core.Security
{
    // <summary>
    /// 中兴管理系统的加解密算法
    /// </summary>
    public class AESS
    {
        private static readonly byte[] IV =
            { 95, 72, 12,45,
            21, 198,214, 104,
            238,251, 37, 176,
            65, 9, 49, 23 };
        private static readonly byte[] Key =
            {
            253,87,220,75,
            215,241,120,121,
            44,91,218,94,
            79,205,73,228,
            28,78,245,98,
            208,251,34,181,
            153,66,1,46,
            211,201,126,31};

        private AESS() { }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Encrypt(string plainText)
        {
            byte[] encrypted;
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encrypted);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="decryptText"></param>
        /// <returns></returns>
        public static string Decrypt(string decryptText)
        {
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDncrypt = new MemoryStream(Convert.FromBase64String(decryptText)))
                {
                    using (CryptoStream csDncrypt = new CryptoStream(msDncrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader swDncrypt = new StreamReader(csDncrypt))
                        {
                            decryptText = swDncrypt.ReadToEnd();
                        }
                    }
                }
            }
            return decryptText;
        }
        /// <summary>
        /// 返回前端前数据加密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string EncryptData(string Text)
        {
            return AESS.Encrypt(Text).Replace("+", "$").Replace("/", "!").Replace("%", "-");
        }
        /// <summary>
        /// 解密前端加密的数据
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string DecryptData(string Text)
        {
            return AESS.Decrypt(Text.Replace("$", "+").Replace("!", "/").Replace("-", "%"));
        }
        public static string createToken()
        {
            Random ro = new Random(10);
            long tick = DateTime.Now.Ticks;
            Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            return ran.Next().ToString();
        }
    }
}
