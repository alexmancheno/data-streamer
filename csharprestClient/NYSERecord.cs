using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace csharprestClient
{

    class NYSERecord
    {
        private RestClient rClient { get; set; }
        private string filePath { get; set; }
        private string fileName { get; set; }
        private int interval { get; set; }

        public NYSERecord(RestClient r, string fp)
        {
            rClient = r;
            filePath = fp;
        }

        public void update()
        {
            HttpWebResponse response = (HttpWebResponse)rClient.makeRequest();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            using (StreamWriter sw = File.AppendText(filePath))
                            {
                                string lastLine = string.Empty;
                                while (!reader.EndOfStream)
                                {
                                    lastLine = reader.ReadLine();
                                }
                                sw.WriteLine(lastLine);
                            }
                        }
                    }
                }
            }
        }
    }
}
