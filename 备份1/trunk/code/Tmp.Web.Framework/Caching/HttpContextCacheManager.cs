using Tmp.Core.Caching;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;

namespace Tmp.Web.Framework.Caching
{
    /// <summary>
    /// Represents a manager for caching between HTTP requests (long term caching)
    /// </summary>
    public partial class HttpContextCacheManager : IHttpContextCacheManager
    {
        /// <summary>
        /// Cache object
        /// </summary>
        protected Cache Cache
        {
            get
            {
                return HttpContext.Current.Cache;
            }
        }
        
        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public virtual T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Cache time</param>
        public virtual void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;

            this.Cache.Insert(key, data, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(cacheTime));
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        public virtual bool IsSet(string key)
        {
            return (this.Cache.Get(key) != null);
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public virtual void Remove(string key)
        {
            this.Cache.Remove(key);
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public virtual void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = new List<String>();

            IDictionaryEnumerator CacheEnum = this.Cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                if (regex.IsMatch(CacheEnum.Key.ToString()))
                    Remove(CacheEnum.Key.ToString());
            }   

        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public virtual void Clear()
        {
            IDictionaryEnumerator CacheEnum = this.Cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                 Remove(CacheEnum.Key.ToString());
            }   
        }
    }
}