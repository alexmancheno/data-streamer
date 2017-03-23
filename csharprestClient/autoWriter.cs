using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace csharprestClient
{
    class autoWriter
    {
        private RestClient rClient { get; set; }
        private string filePath { get; set; }
        private string fileName { get; set; }
        private int interval { get; set; }
        private CancellationTokenSource tokenSource;
        private CancellationToken cancellationToken;

        public autoWriter(RestClient r, string fp, int i)
        {
            rClient = r;
            filePath = fp;
            interval = i;
            tokenSource = new CancellationTokenSource();
            cancellationToken = tokenSource.Token;
        }

        public void updateFilePeriodically()
        {
            Task.Factory.StartNew(() =>
            {
                while (!tokenSource.IsCancellationRequested)
                {
                    Thread.Sleep(interval * 1000);
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



            }, cancellationToken);
        }

        public void stopUpdating()
        {
            tokenSource.Cancel();
        }
    }
}
