using System;
using System.IO;
using System.Net;
using System.Text;

namespace csharprestClient
{
    // This class is to create object instances that can get json responses back from the http endpoint, e.g., Google API.
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

        // This method should return the json object from the endpoint.
        public HttpWebResponse makeRequest()
        {
            string strResponseValue = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endPoint);

            request.Method = httpMethod.ToString();

            return (HttpWebResponse)request.GetResponse();
        }
    }
}
