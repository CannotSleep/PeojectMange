using Tmp.Core.Caching;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;

namespace Tmp.Web.Framework.Caching
{
    public partial class HttpContextSessionManager:IHttpContextSessionManager
    {
        /// <summary>
        /// Cache object
        /// </summary>
        protected HttpSessionState Session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }


        public virtual T Get<T>(string key)
        {
            return (T)Session[key];
        }


        public virtual void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;

            if (Session[key] == null)
            {
                Session.Add(key, data);
                //if (cacheTime != 0)
                //{
                //    ((HttpSessionState)Session[key]).Timeout = cacheTime;
                //}
            }          
        }


        public virtual bool IsSet(string key)
        {
            return (Session[key] != null);
        }

        public IEnumerator GetEnumerator()
        {
            return Session.GetEnumerator();
        }

        public virtual void Remove(string key)
        {
            Session.Remove(key);
        }

        public virtual void RemoveByPattern(string pattern)
        {

            var enumerator = Session.GetEnumerator();
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = new List<String>();
            while (enumerator.MoveNext())
            {
                if (regex.IsMatch(enumerator.Current.ToString()))
                {
                    keysToRemove.Add(enumerator.Current.ToString());
                }
            }

            foreach (string key in keysToRemove)
            {
                Session.Remove(key);
            }
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public virtual void Clear()
        {
            var enumerator = Session.GetEnumerator();
            var keysToRemove = new List<String>();
            while (enumerator.MoveNext())
            {
                keysToRemove.Add(enumerator.Current.ToString());
            }

            foreach (string key in keysToRemove)
            {
                Session.Remove(key);
            }
        }


    }
}
