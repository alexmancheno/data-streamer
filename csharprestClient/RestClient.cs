using System;
using System.IO;
using System.Net;
using System.Text;

namespace csharprestClient
{
    public enum httpVerb
    {
        GET,
        POST
    }

    class RestClient
    {
        public string endPoint { get; set; }
        public httpVerb httpMethod { get; set; }  

        public RestClient()
        {
            endPoint = string.Empty;
            httpMethod = httpVerb.GET;
        }

        public HttpWebResponse makeRequest()
        {
            string strResponseValue = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);

            request.Method = httpMethod.ToString();

            return (HttpWebResponse)request.GetResponse();
        }
    }
}
