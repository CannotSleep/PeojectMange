using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;

namespace Tmp.Core.Util
{
    public class HttpHelper
    {
        public static bool httpProxy = ConfigHelper.GetConfigString("httpProxy") == string.Empty ? false : true;
        public static string httpProxyUser = ConfigHelper.GetConfigString("httpProxyUser") == string.Empty ? "10011637" : ConfigHelper.GetConfigString("httpProxyUser");
        public static string httpProxyPasswd = ConfigHelper.GetConfigString("httpProxyPasswd") == string.Empty ? "as@767852579" : ConfigHelper.GetConfigString("httpProxyPasswd");

        /// <summary>
        /// http请求
        /// </summary>
        /// <param name="url">请求url</param>
        /// <param name="method">请求方法;GET\POST</param>
        /// <param name="requestBody">请求主体</param>
        /// <param name="contentType">请求url</param>
        /// <returns></returns>
        public static string HttpComm(string url, string method, string requestBody, string contentType = "application/json;charset=UTF-8") {
            byte[] data = Encoding.UTF8.GetBytes(requestBody);
            var webRequest = System.Net.WebRequest.Create(url);

            webRequest.Method = method;
            webRequest.Headers.Add("Accept-Language", "zh-cn,zh;q=0.8,en-us;q=0.5,en;q=0.3");
            webRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
            webRequest.ContentLength = data.Length;
            webRequest.ContentType = contentType;

            //代理
            if (httpProxy) {
                System.Net.WebProxy proxy = new System.Net.WebProxy("10.200.251.45:8080/", false);
                proxy.Credentials = new System.Net.NetworkCredential(httpProxyUser, httpProxyPasswd);
                webRequest.Proxy = proxy;
            }

            //请求流
            var writer = webRequest.GetRequestStream();
            writer.Write(data, 0, data.Length);
            writer.Close();


            var webResponse = webRequest.GetResponse();
            var reader = new System.IO.StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
            var readToEnd = reader.ReadToEnd();
            reader.Close();
            webResponse.Close();

            //格式转换返回
            return HttpUtility.HtmlDecode(readToEnd);
        }
    }
}
