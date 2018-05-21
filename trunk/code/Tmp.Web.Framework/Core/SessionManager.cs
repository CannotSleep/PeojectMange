using Tmp.Core.Caching;
using Tmp.Core.Dependency;
using Tmp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace Tmp.Web.Framework.Core
{

    /// <summary>
    /// Session细化项，用于确定哪些session需要清理
    /// </summary>
    /// <remarks></remarks>
    public partial class SessionItem
    {
        public string SessionKey;
        public bool ClearWhenLogout;
    }


    /// <summary>
    /// Session控制表
    /// </summary>
    [Serializable()]
    public partial class SessionManager
    {
        private const string Roles = "Roles";
        private const string User = "User";
        private const string Language = "Language";
        private const string Lock = "Lock";

        private const string UserInfo = "Tmp.UserInfo";
        /// <summary>
        /// Session集合，方便后面扩充 
        /// </summary>
        /// <remarks></remarks>

        private static HashSet<SessionItem> SessionItems = new HashSet<SessionItem>();
        /// <summary>
        /// 添加Session项到集合， 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="value"></param>
        /// <remarks></remarks>
        public static void AddSessionItem<T>(SessionItem item, T value)
        {
            SessionItems.Add(item);
            HttpContext context = HttpContext.Current;
            if ((context != null) & (context.Session != null))
            {
                context.Session[item.SessionKey] = value;
            }
        }

        public static T GetSessionItem<T>(string key)
        {
            var _cardInfoItem = new SessionItem();
            _cardInfoItem.SessionKey = key;
            HttpContext context = HttpContext.Current;
            if ((context != null) & (context.Session != null))
            {
                return (T)context.Session[_cardInfoItem.SessionKey];
            }
            return default(T);
        }

        /// <summary>
        /// 登出登陆处理需要清理的Session， 
        /// </summary>
        /// <remarks></remarks>
        public static void Logout()
        {
            foreach (var item in SessionItems)
            {
                if (item.ClearWhenLogout)
                {
                    HttpContext context = HttpContext.Current;
                    if ((context != null) & (context.Session != null))
                    {
                        context.Session.Remove(item.SessionKey);
                    }
                }
            }
            //清空Session CJ
            IocObjectManager.GetInstance().Resolve<IHttpContextSessionManager>().Clear();
        }



        ///// <summary>
        ///// 添加Roles
        ///// </summary>
        ///// <param name="model"></param>
        ///// <remarks></remarks>
        //public static void AddRoles(Roles model)
        //{
        //    if (model == null)
        //    {
        //        throw new ArgumentException("参数不能为NULL");
        //    }
        //    HttpContext context = HttpContext.Current;
        //    if ((context != null) & (context.Session != null))
        //    {
        //        context.Session[Roles] = model;
        //    }
        //}
        /// <summary>
        /// 删除Roles
        /// </summary>
        /// <remarks></remarks>
        public static void RemoveRoles()
        {
            HttpContext context = HttpContext.Current;
            if ((context != null) & (context.Session != null))
            {
                context.Session.Remove(Roles);
            }
        }
        ///// <summary>
        ///// 得到Roles
        ///// </summary>
        ///// <returns></returns>
        ///// <remarks></remarks>
        //public static Roles GetRoles()
        //{
        //    HttpContext context = HttpContext.Current;
        //    if ((context != null) & (context.Session != null))
        //    {
        //        return (Roles)context.Session[Roles];
        //    }
        //    return null;
        //}
        ///// <summary>
        ///// 添加User
        ///// </summary>
        ///// <param name="model"></param>
        ///// <remarks></remarks>
        //public static void AddUser(Account model)
        //{
        //    if (model == null)
        //    {
        //        throw new ArgumentException("参数不能为NULL");
        //    }
        //    HttpContext context = HttpContext.Current;
        //    if ((context != null) & (context.Session != null))
        //    {
        //        context.Session[User] = model;
        //    }
        //}
        /// <summary>
        /// 删除User
        /// </summary>
        /// <remarks></remarks>
        public static void RemoveUser()
        {
            HttpContext context = HttpContext.Current;
            if ((context != null) & (context.Session != null))
            {
                context.Session.Remove(User);
            }
        }
        ///// <summary>
        ///// 得到User
        ///// </summary>
        ///// <returns></returns>
        ///// <remarks></remarks>
        //public static Account GetUser()
        //{
        //    HttpContext context = HttpContext.Current;
        //    if ((context != null) & (context.Session != null))
        //    {
        //        return (Account)context.Session[User];
        //    }
        //    return null;
        //}
        /// <summary>
        /// 添加Language
        /// </summary>
        /// <param name="lan"></param>
        /// <remarks></remarks>
        public static void AddLanguage(string lan)
        {
            if (lan == null)
            {
                throw new ArgumentException("参数不能为NULL");
            }
            HttpContext context = HttpContext.Current;
            if ((context != null) & (context.Session != null))
            {
                context.Session[Language] = lan;
            }
        }
        /// <summary>
        /// 删除Language
        /// </summary>
        /// <remarks></remarks>
        public static void RemoveLanguage()
        {
            HttpContext context = HttpContext.Current;
            if ((context != null) & (context.Session != null))
            {
                context.Session.Remove(Language);
            }
        }
        /// <summary>
        /// 得到Language
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetLanguage()
        {
            HttpContext context = HttpContext.Current;
            if ((context != null) & (context.Session != null))
            {
                return (string)context.Session[Language];
            }
            return null;
        }
        /// <summary>
        /// 添加锁定窗口
        /// </summary>
        /// <param name="isLock"></param>
        /// <remarks></remarks>
        public static void AddLock(string isLock)
        {
            if (isLock == null)
            {
                throw new ArgumentException("参数不能为NULL");
            }
            HttpContext context = HttpContext.Current;
            if ((context != null) & (context.Session != null))
            {
                context.Session[Lock] = isLock;
            }
        }
        /// <summary>
        /// 删除锁定窗口
        /// </summary>
        /// <remarks></remarks>
        public static void RemoveLock()
        {
            HttpContext context = HttpContext.Current;
            if ((context != null) & (context.Session != null))
            {
                context.Session.Remove(Lock);
            }
        }
        /// <summary>
        ///是否锁定窗口
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        /// //todo
        public static string GetLock()
        {
            HttpContext context = HttpContext.Current;
            if ((context != null) & (context.Session != null))
            {
                return (string)context.Session[Lock];
            }
            return "";
        }

        

        public static void AddUserInfo(DataRow userDataRow)
        {
            if (userDataRow == null)
            {
                throw new ArgumentException("参数不能为NULL");
            }
            var context = HttpContext.Current;
            if (context != null && context.Session != null)
            {
                context.Session[UserInfo] = userDataRow;
            }
        }

        public static void RemoveUserInfo(DataRow userDataRow)
        {
            var context = HttpContext.Current;
            if (context != null && context.Session != null)
            {
                context.Session.Remove(UserInfo);
            }
        }

        public static DataRow GetUserInfo()
        {
            var context = HttpContext.Current;
            if (context != null && context.Session != null)
            {
                return (DataRow)context.Session[UserInfo];
            }
            return null;
        }
    }
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
