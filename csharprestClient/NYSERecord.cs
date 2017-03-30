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

        private string companyName { get; set; }

        public NYSERecord(RestClient r, string fp, string cn)
        {
            rClient = r; //this object is used to make the api call to the Google Server and retrive JSON object
            filePath = fp; //absolute file path to the text file
            companyName = cn; //currently unused, but perhaps having company name can be useful a little later on
        }

        public void initializeRecord()
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
                            using (StreamWriter sw = File.CreateText(filePath))
                            {
                                string currentLine = string.Empty;
                                while (currentLine != null) //write each line to the 'sw' text file.
                                {
                                    
                                    currentLine = reader.ReadLine(); 
                                    sw.WriteLine(currentLine);
                                }
                            }
                        }
                    }
                }
            }
        }

        //this method will go unused since it seems Google will not let us spam API calls to their servers =(
        public void update()
        {
            try
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

            catch (Exception e)
            {

            }
        }
    }
}
