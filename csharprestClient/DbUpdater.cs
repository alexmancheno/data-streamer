using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace csharprestClient
{
    class DbUpdater
    {
        private static DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

        public static void updateTable(DataRow record, string IntradayConnectionString)
        {
            RestClient rClient = new RestClient();
            rClient.endPoint = "http://chartapi.finance.yahoo.com/instrument/1.0/[TICKER]/chartdata;type=quote;range=1d/csv";

            HttpWebResponse response = (HttpWebResponse)rClient.makeRequest();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            using (SqlConnection connection = new SqlConnection(IntradayConnectionString))
                            {
                                string nameOfTable = (string) record[2];
                                SqlCommand command = new SqlCommand();
                                command.Connection = connection;

                                string line;
                                char[] separatingChars = { ',' };
                                string substringToDetect = "volume:";
                                bool substringDetectedPreviousLine = false;
                                int gmtOffset = 0;

                                connection.Open();

                                for (int i = 0; (line = reader.ReadLine()) != null && line != ""; i++)
                                {
                                    string[] words = line.Split(separatingChars, StringSplitOptions.RemoveEmptyEntries);
                                    if (line.Contains("error"))
                                    {
                                        command.CommandText = String.Format("UPDATE Stock_List SET Error = 1 WHERE Storage_Ticker = '{0}';", nameOfTable);
                                        break;
                                    }

                                    else if (line.Contains("gmtoffset:"))
                                    {
                                        char[] separatingChar = { ':' };
                                        string[] arrayContainingOffset = "gmtoffset:-14400".Split(separatingChar, StringSplitOptions.RemoveEmptyEntries);
                                        gmtOffset = Convert.ToInt32(arrayContainingOffset[1]);
                                    }

                                    if (substringDetectedPreviousLine)
                                    {
                                        try
                                        {
                                            DateTime date = FromUnixTime(Convert.ToInt64(words[0]));
                                            string formattedDate = date.ToString("d");
                                            string formattedTime = date.ToString("HH:mm:ss");

                                            command.CommandText = String.Format("INSERT INTO {0} ([Date] date, [Time] time, [Close], [High], [Low], [Open], [Volume]) values ('{1}', '{2}', {3}, {4}, {5}, {6});", nameOfTable, formattedDate, formattedTime, words[1], words[2], words[3], words[4], words[5]);
                                            command.ExecuteNonQuery();
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine("Error trying to insert into " + (string)record[1] + ": " + e.StackTrace);
                                        }
                                    }
                                    if (line.Contains(substringToDetect)) substringDetectedPreviousLine = true;
                                }

                                command.Dispose();
                            }
                        }
                    }
                }
            }
        }

        public static void createTable(DataRow record, string IntradayConnectionString)
        {

            RestClient rClient = new RestClient();
            
            string httpString = @"http://chartapi.finance.yahoo.com/instrument/1.0/[TICKER]/chartdata;type=quote;range=1d/csv";
            rClient.endPoint = httpString.Replace("[TICKER]", (string) record[1]);
            Console.WriteLine("Ticker: " + (string)record[1]);

            HttpWebResponse response = (HttpWebResponse)rClient.makeRequest();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            using (SqlConnection connection = new SqlConnection(IntradayConnectionString))
                            {
                                string nameOfTable = (string) record[2];
                                string primaryKey = nameOfTable + "_data_ID";
                                int stockID = (int) record[0];

                                SqlStatements sqs = new SqlStatements();
                                sqs.createTableSqlCommand(nameOfTable, primaryKey, IntradayConnectionString);

                                string updateInitializedColumnString = String.Format("UPDATE Stock_List SET Initialized = 1 WHERE Storage_Ticker = '{0}';", nameOfTable);

                                Console.WriteLine("Attempting to update Initialized column");
                                // Execute the command to update 'Initialed' column from the 'Stock_List' table:

                                SqlCommand command = new SqlCommand(updateInitializedColumnString, connection);

                                command.CommandText = updateInitializedColumnString;
                                //command.ExecuteNonQuery();
        
                                string line;
                                char[] separatingChars = { ',' };
                                string substringToDetect = "volume:";
                                int gmtOffset = 0;
                                bool substringDetectedPreviousLine = false;

                                for (int i = 0; (line = reader.ReadLine()) != null && line != ""; i++)
                                {
                                    string[] words = line.Split(separatingChars, StringSplitOptions.RemoveEmptyEntries);

                                    if (line.Contains("error"))
                                    {
                                        command.CommandText = String.Format("UPDATE Stock_List SET Error = 1 WHERE Storage_Ticker = '{0}';", nameOfTable);
                                        break;
                                    }

                                    else if (line.Contains("gmtoffset:"))
                                    {
                                        char[] separatingChar = { ':' };
                                        string[] arrayContainingOffset = "gmtoffset:-14400".Split(separatingChar, StringSplitOptions.RemoveEmptyEntries);
                                        gmtOffset = Convert.ToInt32(arrayContainingOffset[1]);
                                    }

                                    if (substringDetectedPreviousLine)
                                    {
                                        try
                                        {
                                            DateTime date = FromUnixTime(Convert.ToInt64(words[0]) + gmtOffset);
                                            string formattedDate = date.ToString("d");
                                            string formattedTime = date.ToString("HH:mm:ss");

                                            Console.WriteLine(formattedDate + " - " + formattedTime + " - " + words[1] + " - " +words[2] + " - " + words[3] + " - " + words[4] + " - " + words[5]);

                                            command.CommandText = String.Format("INSERT INTO {0} ([Date], [Time], [Close], [High], [Low], [Open], [Volume], Stock_ID) VALUES ('{1}', '{2}', {3}, {4}, {5}, {6}, {7}, {8});", nameOfTable, formattedDate, formattedTime, words[1], words[2], words[3], words[4], words[5], stockID);
                                            command.ExecuteNonQuery();
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine("Error trying to insert into "+ (string) record[1] + " - "+ e.InnerException + " : \n"+ e.StackTrace);
                                        }
                                    }
                                    if (line.Contains(substringToDetect)) substringDetectedPreviousLine = true;
                                }
                                command.Dispose();
                            }
                        }
                    }
                }
            }
        }
    }
}
