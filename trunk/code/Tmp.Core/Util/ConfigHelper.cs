using Tmp.Core.Caching;
using Tmp.Core.Dependency;
using System;
using System.Configuration;

namespace Tmp.Core.Util
{
    /// <summary>
    /// web.config操作类
    /// Copyright (C) Maticsoft
    /// </summary>
    public  class ConfigHelper
    {
        /// <summary>
        /// 得到AppSettings中的配置字符串信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigString(string key)
        {
            string CacheKey = "AppSettings-" + key;

            var cacheManager = IocObjectManager.GetInstance().Resolve<IHttpContextCacheManager>();
            object objModel = cacheManager.Get<object>(CacheKey, 180, () =>
            {
                return ConfigurationManager.AppSettings[key];
            });
           
            return objModel.ToString();
        }

        /// <summary>
        /// 得到AppSettings中的配置Bool信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetConfigBool(string key)
        {
            bool result = false;
            string cfgVal = GetConfigString(key);
            if (cfgVal != null && string.Empty != cfgVal)
            {
                try
                {
                    result = bool.Parse(cfgVal);
                    // Ignore format exceptions.
                }
                catch (FormatException generatedExceptionName)
                {
                }
            }
            return result;
        }
        /// <summary>
        /// 得到AppSettings中的配置Decimal信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal GetConfigDecimal(string key)
        {
            decimal result = 0;
            string cfgVal = GetConfigString(key);
            if (cfgVal != null && string.Empty != cfgVal)
            {
                try
                {
                    result = decimal.Parse(cfgVal);
                    // Ignore format exceptions.
                }
                catch (FormatException generatedExceptionName)
                {
                }
            }

            return result;
        }
        /// <summary>
        /// 得到AppSettings中的配置int信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetConfigInt(string key)
        {
            int result = 0;
            string cfgVal = GetConfigString(key);
            if (cfgVal != null && string.Empty != cfgVal)
            {
                try
                {
                    result = int.Parse(cfgVal);
                    // Ignore format exceptions.
                }
                catch (FormatException generatedExceptionName)
                {
                }
            }

            return result;
        }
    }
}

