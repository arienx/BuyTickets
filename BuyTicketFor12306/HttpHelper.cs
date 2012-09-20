using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Drawing;

namespace BuyTicketFor12306
{
    class HttpHelper
    {
        private static CookieContainer container = new CookieContainer();

        public static Stream GetResponseStream(string url, bool useGET = true, string param = "")
        {
            //导入证书
            X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            X509Certificate certs = X509Certificate.CreateFromCertFile("srca.cer");

            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            //向请求中追加证书
            request.ClientCertificates.Add(certs);

            //为保持会话状态
            request.CookieContainer = container;

            if (!useGET)
            {
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                byte[] requestData = Encoding.Default.GetBytes(param);
                request.ContentLength = requestData.Length;

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(requestData, 0, requestData.Length);
                }
            }

            HttpWebResponse response = null;
            try
            {
                response = request.GetResponse() as HttpWebResponse;
                response.Cookies = request.CookieContainer.GetCookies(new Uri(url, UriKind.RelativeOrAbsolute));
            }
            catch (WebException ex) { throw ex; }

            container = request.CookieContainer;

            return response.GetResponseStream();
        }

        /// <summary>
        /// 获取响应图片
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Image GetResponseImage(string url, bool useGET = true, string param = "")
        {
            Stream stream = null;
            try
            {
                stream = GetResponseStream(url, useGET, param);
            }
            catch (WebException ex)
            {
                throw ex;
            }

            return StreamHelper.ConvertStreamToImage(stream);
        }

        /// <summary>
        /// 获取响应HTML页面路径
        /// </summary>
        /// <param name="url"></param>
        /// <param name="useGET"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string GetResponseHTML(string url, bool useGET = true, string param = "")
        {
            Stream stream = null;
            try
            {
                stream = GetResponseStream(url, useGET, param);
            }
            catch (WebException ex)
            {
                throw ex;
            }
            return StreamHelper.ConvertStreamToHTMLFile(stream);
        }
    }
}
