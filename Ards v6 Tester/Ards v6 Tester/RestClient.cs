using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ards_v6_Tester
{
    public class RestClient
    {
        public static string DoPost(string requestUrl, string requestBody, string authToken)
        {
            try
            {
                var httpWReq = (HttpWebRequest)WebRequest.Create(requestUrl);

                var encoding = new ASCIIEncoding();
                string postData = requestBody;
                byte[] data = encoding.GetBytes(postData);

                httpWReq.Method = "POST";
                httpWReq.Accept = "application/json";
                httpWReq.ContentType = "application/json";
                httpWReq.ContentLength = data.Length;
                httpWReq.Headers.Add("Authorization", authToken);

                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)httpWReq.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    try
                    {
                        var x = JToken.Parse(responseString).ToString(Newtonsoft.Json.Formatting.Indented);
                        return x;
                    }
                    catch
                    {
                        return responseString;
                    }
                }
                return string.Empty;

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static string DoPut(string requestUrl, string requestBody, string authToken)
        {
            try
            {
                var httpWReq = (HttpWebRequest)WebRequest.Create(requestUrl);

                var encoding = new ASCIIEncoding();
                string postData = requestBody;
                byte[] data = encoding.GetBytes(postData);

                httpWReq.Method = "PUT";
                httpWReq.Accept = "application/json";
                httpWReq.ContentType = "application/json";
                httpWReq.Headers.Add("Authorization", authToken);
                httpWReq.ContentLength = data.Length;

                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)httpWReq.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    try
                    {
                        var x = JToken.Parse(responseString).ToString(Newtonsoft.Json.Formatting.Indented);
                        return x;
                    }
                    catch
                    {
                        return responseString;
                    }
                }
                return string.Empty;

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        public static string DoGet(string requestUrl, string authToken)
        {
            try
            {
                var httpWReq = (HttpWebRequest)WebRequest.Create(requestUrl);

                var encoding = new ASCIIEncoding();

                httpWReq.Method = "GET";
                httpWReq.Accept = "application/json";
                httpWReq.ContentType = "application/json";
                httpWReq.Headers.Add("Authorization", authToken);
                
                var response = (HttpWebResponse)httpWReq.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    try
                    {
                        var x = JToken.Parse(responseString).ToString(Newtonsoft.Json.Formatting.Indented);
                        return x;
                    }
                    catch
                    {
                        return responseString;
                    }
                }
                return string.Empty;

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static string DoRemove(string requestUrl, string authToken)
        {
            try
            {
                var httpWReq = (HttpWebRequest)WebRequest.Create(requestUrl);

                var encoding = new ASCIIEncoding();

                httpWReq.Method = "DELETE";
                httpWReq.Accept = "application/json";
                httpWReq.ContentType = "application/json";
                httpWReq.Headers.Add("Authorization", authToken);

                var response = (HttpWebResponse)httpWReq.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    try
                    {
                        var x = JToken.Parse(responseString).ToString(Newtonsoft.Json.Formatting.Indented);
                        return x;
                    }
                    catch
                    {
                        return responseString;
                    }
                }
                return string.Empty;

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
