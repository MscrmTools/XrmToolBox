using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

namespace XrmToolBox.ToolLibrary.AppCode
{
    public class WebRequestHelper
    {
        public static string MakeGet(string url, NameValueCollection queryStringParameters = null, NameValueCollection requestHeaders = null)
        {
            string responseText;
            using (WebClient client = new WebClient())
            {
                try
                {
                    if (requestHeaders?.Count > 0)
                    {
                        foreach (string header in requestHeaders.AllKeys)
                            client.Headers.Add(header, requestHeaders[header]);
                    }

                    if (queryStringParameters?.Count > 0)
                    {
                        foreach (string parm in queryStringParameters.AllKeys)
                            client.QueryString.Add(parm, queryStringParameters[parm]);
                    }
                    byte[] bytes = client.DownloadData(url);
                    responseText = Encoding.UTF8.GetString(bytes);
                }
                catch (WebException ex)
                {
                    throw new Exception($"Erreur durant le GET suivant : {url} Token : {requestHeaders?[0]} Détail : {ex.Message}");
                }
            }

            return responseText;
        }

        public static HttpWebResponse MakeRequest(string url, string pJson, string pMethod, string headerAccessTokenKey = null, string pAccessToken = null)
        {
            return MakeRequestWithContentType(url, pJson, "application/json", pMethod, headerAccessTokenKey, pAccessToken);
        }

        public static HttpWebResponse MakeRequestWithContentType(string url, string pJson, string contentType, string pMethod, string headerAccessTokenKey = null, string pAccessToken = null)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            httpWebRequest.Accept = "application/json";
            httpWebRequest.Method = pMethod;

            if (!string.IsNullOrEmpty(headerAccessTokenKey) && !string.IsNullOrEmpty(pAccessToken))
            {
                httpWebRequest.Headers.Add(headerAccessTokenKey, pAccessToken);
            }

            if (pJson?.Length > 0)
            {
                httpWebRequest.ContentType = contentType;
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(pJson);
                }
            }

            return (HttpWebResponse)httpWebRequest.GetResponse();
        }
    }
}