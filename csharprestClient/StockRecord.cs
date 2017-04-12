using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

namespace csharprestClient
{


    class StockRecord
    {
        private RestClient rClient { get; set; }
        private string filePath { get; set; }

        private string ticker { get; set; }

        public StockRecord(RestClient r, string fp, string t)
        {
            rClient = r; //this object is used to make the api call to the Google Server and retrive JSON object
            filePath = fp; //absolute file path to the text file
            ticker = t; //currently unused, but perhaps having company name can be useful a little later on
        }

        public string getFilePath()
        {
            return filePath;
        }

        public void initializeRecord()
        {
            //create database table
            //using (SqlConnection connection = new SqlConnection(@"Data Source=Alex-PC\SQLExpress;Initial Catalog=numeraxial;Integrated Security=True"))
            //{
            //    string sqlString = String.Format("create table {0} (business_date varchar(22), closing decimal, high decimal, low decimal, opening decimal, volume int);", ticker);
            //    //string sqlString = String.Format("CREATE TABLE {0} (date varchar(20), close decimal, high decimal, low decimal, open decimal, volume int);", ticker);
            //    using (SqlCommand cmd = new SqlCommand(sqlString, connection))
            //    {
            //        try
            //        {
            //            connection.Open();
            //            cmd.ExecuteNonQuery();
            //        }
            //        catch (Exception ex)
            //        {

            //        }
            //    }
            //}
            HttpWebResponse response = (HttpWebResponse)rClient.makeRequest();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            try
                            {
                                using (StreamWriter sw = File.CreateText((@filePath)))
                                {
                                    string currentLine = string.Empty;
                                    for (int i = 0; (currentLine = reader.ReadLine()) != null && currentLine != ""; i++)
                                    {
                                        sw.WriteLine(currentLine); //write each line to file
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                
                            }
                        }
                    }
                }
            }
        }
    

        public void update()
        {
            if (!File.Exists(filePath))
            {
                initializeRecord();
            }
            else
            {
                try
                {
                    rClient.endPoint = rClient.endPoint.Replace("15d", "1d");
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
                                        string currentLine = string.Empty;
                                        for (int i = 0; (currentLine = reader.ReadLine()) != null && currentLine != ""; i++)
                                        {
                                            if (i >= 17)
                                            {
                                                sw.WriteLine(currentLine); //write each line to file
                                            }
                                        }
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
}

//currently unused, but might come useful: 


//if (i >= 0)
//{
//words = currentLine.Split(separatingChars, StringSplitOptions.RemoveEmptyEntries);
//string sqli = String.Format("insert into {0} (business_date, closing, high, low, opening, volume) values ('{1}', {2}, {3}, {4}, {5}, {6});", ticker, words[0], words[1], words[2], words[3], words[4], words[5]);
//using (SqlConnection connection = new SqlConnection(@"Data Source=Alex-PC\SQLExpress;Initial Catalog=numeraxial;Integrated Security=True"))
//{
//    using (SqlCommand sqlCmd = new SqlCommand(sqli, connection))
//    {
//        connection.Open();
//        sqlCmd.ExecuteNonQuery();
//    }
//}