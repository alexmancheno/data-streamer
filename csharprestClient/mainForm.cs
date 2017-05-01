using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.Data;

namespace csharprestClient
{

    public partial class GoogleApiCaller : Form
    { 
        //properties of the form, which are used to perform the various operations of this same form
        private Dictionary<string, string> apiDictionary = new Dictionary<string, string>();
        private List<StockRecord> yahooRecordList = new List<StockRecord>(45000); // the list Yahoo Finance's api will handle
        private int updateHour, updateMinute;
        private RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
        private string configFolder = @"C:\Numeraxial Data Streamer";
        private string settingsFile = @"C:\Numeraxial Data Streamer\settings.txt";

        private string NumeraxialConnectionString = @"Data Source=ALEX-PC;Initial Catalog=Numeraxial;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private string IntradayConnectionString = @"Data Source=ALEX-PC;Initial Catalog=Intraday;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private string[] currentSettings = new string[5];
        /**
        currentSettings[0] --> string containing file paths to ticker-list text files, separated by commas
        currentSettings[1] --> string containing update time (hour and minute, separated by comma)    
        */

        private List<string> listFiles = new List<string>();

        // Days this app should NOT update records (to avoid getting duplicate data)
        private DateTime[] americanHolidays =
        {
            new DateTime(2017, 4, 14),  // Good Friday
            new DateTime(2017, 5, 29),  // Memorial Day
            new DateTime(2017, 7, 3),   // Early close (closes at 1pm)
            new DateTime(2017, 7, 4),   // Independence Day
            new DateTime(2017, 9, 4),   // Labor Day
            new DateTime(2017, 11, 23), // Thanksgiving Day
            new DateTime(2017, 11, 24), // Early close (closes at 1pm)
            new DateTime(2017, 12, 25)  // Christmas Day
        };


        private int[,] closingTimes = new int[,]
        {
            {12, 35}, // For American exchanges.
            {13, 50 }
            
        };
        


        public GoogleApiCaller()
        {
            reg.SetValue("Numeraxial Data Streamer", Application.ExecutablePath.ToString());
            InitializeComponent();
            apiDictionary.Add("Google Finance", "https://www.google.com/finance/getprices?i=[PERIOD]&p=[DAYS]d&f=d,o,h,l,c,v&def=cpct&q=[TICKER]");
            apiDictionary.Add("Yahoo Finance", "http://chartapi.finance.yahoo.com/instrument/1.0/[TICKER]/chartdata;type=quote;range=[DAYS]/csv");
            apiDictionary.Add("Alphavantage", "http://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=[TICKER]&interval=1min&outputsize=full&apikey=2858");

            // Check to see whether or not the app should be startup with Windows
            if (reg.GetValue("Numeraxial Data Streamer") == null)
            {
                // If it's not checked, the app will not be in startup state
                chkStartup.Checked = false;
            }
            else
            {
                // If it is checked, the app will be in startup state
                chkStartup.Checked = true;
            }

            // Read settings from previous state of the program (if it exists)
           // loadSettings(configFolder, settingsFile);

            // Start updating periodically
            startTimer();
        }
        
        // ----Start of utility functions----- 

        private void startTimer()
        {
            bool[] wasUpdatedRecently = new bool[2];

            Task.Run(() =>
            {
                while (true)
                {
                    // Updater for American stocks
                    if ((DateTime.Now.Hour == closingTimes[0, 0] && DateTime.Now.Minute == closingTimes[0, 1]) && DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday && todayIsNotHoliday() && !wasUpdatedRecently[0])
                    {
                        debugOutPut("Fetching USA stocks. It's now: " + DateTime.Now);
                        startUpdating("USA");
                        wasUpdatedRecently[0] = true;
                    }
                    else
                    {
                        wasUpdatedRecently[0] = false;
                    }

                    // Updater for 
                    if ((DateTime.Now.Hour == closingTimes[1, 0] && DateTime.Now.Minute == closingTimes[1, 1]) && DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday && todayIsNotHoliday() && !wasUpdatedRecently[1])
                    {
                        debugOutPut("Fetching ");
                        startUpdating("Danny");
                        wasUpdatedRecently[1] = true;
                    }
                    else
                    {
                        wasUpdatedRecently[1] = false;
                    }
                    Thread.Sleep(30000);
                }

            });

        }

        private void startUpdating(string country)
        {
            Task.Run(() =>
            {
                SqlDataReader reader;
                using (SqlConnection connection = new SqlConnection(NumeraxialConnectionString))
                {
                    string queryString = String.Format("SELECT * FROM Stock_List1 WHERE Country = '{0}'", country);
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(queryString, connection))
                    {
                        try
                        {
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {

                                IDataRecord record = (IDataRecord)reader;
                                
                                if ((bool) record[7])   // If the table has been created, update it.
                                {
                                    //debugOutPut("Trying to update table.");
                                    DbUpdater.updateTable(record, IntradayConnectionString, NumeraxialConnectionString);
                                    //debugOutPut("Updating table..");
                                }
                                else                    // Else, pull last 2 weeks of data and create the table.
                                {
                                    //debugOutPut("Trying to create table.");
                                    DbUpdater.createTable(record, IntradayConnectionString, NumeraxialConnectionString);
                                    //debugOutPut("Creating table..");
                                }
                                Thread.Sleep(10000);
                            }
                        } 
                        catch (Exception e)
                        {
                            debugOutPut(e.ToString());
                        }
                    }
                }
            });
        }

        private bool todayIsNotHoliday()
        {
            for (int i = 0; i < americanHolidays.Length; i++)
            {
                if (DateTime.Now.Date == americanHolidays[i].Date)
                    return false;
            }
            return true;
        }

        private void updateRecords()
        {

            using (StreamWriter sw = File.CreateText(txtLocationOfFilePaths.Text + "stock-file-paths.txt"))
            {
                foreach (StockRecord record in yahooRecordList)
                {
                    record.update();
                    //sw.WriteLine(record.getFilePath());
                }
            }
        }

        private void addListToQueue(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    debugOutPut("->" + path + " was read successfully.");
                    string currentLine;
                    char[] seperatingChars = { '\t' };
                    while ((currentLine = sr.ReadLine()) != null)
                    {
                        string[] words = currentLine.Split(seperatingChars, StringSplitOptions.RemoveEmptyEntries);
                        string ep = apiDictionary["Yahoo Finance"];
                        //ep = ep.Replace("[PERIOD]", "15");
                        ep = ep.Replace("[TICKER]", words[0]);
                        ep = ep.Replace("[DAYS]", "15d");
                        RestClient rClient = new RestClient();
                        rClient.endPoint = ep;
                        string filePath = txtSaveLocation.Text + words[0] + ".txt";
                        words[0] = words[0].Replace('-', '_');
                        yahooRecordList.Add(new StockRecord(rClient, filePath, words[0])); //create new StockRecord with the current line's 
                        //information, the ticker and company name, which is enough to create the database table and text file.
                    }
                    debugOutPut("-> Tickers from the list file have been added to queue.");

                    listFiles.Add(path); // To keep track of list file paths to save to settings 
                }
            }
            catch (Exception ex)
            {
                debugOutPut("-> There was an error " + txtListFile.Text + ": " + ex.ToString());
            }
        }

        private void loadSettings(string cf, string sf) 
        {
            if (!Directory.Exists(cf))
            {
                Directory.CreateDirectory(cf);
                if (!File.Exists(sf))
                {
                    File.Create(sf);
                }
            }
            else
            {
                // Load previous session's settings to be our current session's settings
                currentSettings = File.ReadAllLines(sf); // This will assign 'currentSettings' a new string array
                
                // Load each ticker-list text file to the queue
                listFiles = new List<string>(currentSettings[0].Split(','));
                foreach (string listFile in listFiles)
                {
                    addListToQueue(listFile);
                }

                // Load update-time from last session
                string[] updateTime = currentSettings[1].Split(',');
                setUpdateTime(updateTime[0], updateTime[1]);
            }
        }

        private void setUpdateTime(string hour, string minute)
        {
            int h, m;
            bool hourIsNumeric, minuteIsNumeric;
            if ((hourIsNumeric = int.TryParse(hour, out h)) && (minuteIsNumeric = int.TryParse(minute, out m)) && h >= 0 && h <= 23 && m >= 0 && m <= 59)
            {
                updateHour = h;
                updateMinute = m;
                currentSettings[1] = hour + "," + minute;
                debugOutPut("-> Current update-time: " + hour + ":" + minute);
            }
            else
            {
                debugOutPut("-> Incorrect update-time input.");
            }
        }

        // ----End of utility functions----

        // ----Start of overriden default C# functions----
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Prevent form from closing  
            e.Cancel = true;

            //using (StreamWriter sw = File.CreateText(settingsFile))
            //{
            //    sw.WriteLine(String.Join(",", listFiles));
            //    sw.WriteLine(currentSettings[1]);
            //}

            // Now close the form
            e.Cancel = false;
        }
        // ----End of overriden default C# functions----

        #region UI Event Handlers

        //this button is currently not used, but I'll fix it a little later
        private void cmdGO_Click(object sender, EventArgs e)
        {
            //replace substrings with user input from the desktop gui
            string selectedRequest = apiDictionary[comboBoxOptions.Text];
            selectedRequest = selectedRequest.Replace("[TICKER]", txtTicker.Text);
            if (apiDictionary[comboBoxOptions.Text].Contains("[DAYS]"))
                selectedRequest = selectedRequest.Replace("12000", txtDays.Text);
            //if (apiDictionary[comboBoxOptions.Text].Contains("[PERIOD]"))
            //    selectedRequest = selectedRequest.Replace("60", txtInterval.Text);

            //create ep and add to queue 
            RestClient rClient = new RestClient();
            rClient.endPoint = selectedRequest;
            string filePath = txtFilePath.Text + txtTicker.Text;

            //stockQueue.Add(txtTicker.Text, new NYSERecord(rClient, filePath));
        } 
        #endregion

        //this is used to output to the 'log' in the first tab of the form, sort of like outputting to the terminal.
        public void debugOutPut(string strDebugText)
        {
            try
            {
                //System.Diagnostics.Debug.Write(strDebugText + Environment.NewLine);
                txtResponse.Text = txtResponse.Text + strDebugText + Environment.NewLine;
                txtResponse.SelectionStart = txtResponse.TextLength;
                txtResponse.ScrollToCaret();
            }
            catch (Exception ex)
            {
                //System.Diagnostics.Debug.Write(ex.Message, ToString() + Environment.NewLine);
            }
        }

        private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
        {

        }

        private void GoogleApiCaller_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        //this is to select a save location for the single record from the first tab of the form.
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.RootFolder = Environment.SpecialFolder.Desktop;
                fbd.Description = "Select a folder where the *.txt file will be saved";
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    txtFilePath.Text = fbd.SelectedPath + "\\";
            }
             catch (Exception ex)
            {
                debugOutPut("-> There was an error selecting the file: " + ex.ToString());
            } 

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // To remove a list text file from the queue record tab
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listActiveFiles.Items.Count > 0)
            {
                try
                {
                    ListViewItem item = listActiveFiles.SelectedItems[0];
                    string fileName = item.SubItems[0].Text;
                    debugOutPut(fileName);
                    listActiveFiles.Items.Remove(item);
                } 
                catch (Exception ex)
                {
                    debugOutPut("-> There was an error killing the process: " + ex.ToString());
                }
            }

            
        }

        private void lblListFilePath_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.RootFolder = Environment.SpecialFolder.Desktop;
                fbd.Description = "Select a folder where the *.txt file will be saved";
                if (fbd.ShowDialog() == DialogResult.OK)
                    txtSaveLocation.Text = fbd.SelectedPath + "\\";
            }
            catch (Exception ex)
            {
                debugOutPut("-> There was an error selecting the save location: " + ex.ToString());
            }
        }

        private void btnSelectListFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtListFile.Text = ofd.FileName.ToString();
                }
            }
            catch (Exception ex)
            {
                debugOutPut("-> There was an error selecting the list file: " + ex.ToString());
            }
        }

        //this is the button that does the heavy lifting: it first adds in all the files into 'stockQueue' 
        private void btnCreateRecords_Click(object sender, EventArgs e)
        {
            addListToQueue(txtListFile.Text);
        }

        private void txtListFile_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSetUpdateTime_Click(object sender, EventArgs e)
        {
            setUpdateTime(txtHour.Text, txtMinute.Text);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            try
            {

                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.RootFolder = Environment.SpecialFolder.Desktop;
                fbd.Description = "Select a folder where the *.txt file will be saved";
                if (fbd.ShowDialog() == DialogResult.OK)
                    txtLocationOfFilePaths.Text = fbd.SelectedPath + "\\";
            }
            catch (Exception ex)
            {
                debugOutPut("-> There was an error selecting the save location: " + ex.ToString());
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkStartup.Checked)
            {
                // If the 'Update Daily' checkbox is checked, the app will startup with Windows
                reg.SetValue("Numeraxial Data Streamer", Application.ExecutablePath);
            }
            else
            {
                // If it isn't, the app will not startup with Windows
                reg.DeleteValue("Numeraxial Data Streamer");
            }
        }

        private void tabCreateRecord_Click(object sender, EventArgs e)
        {

        }
    }
}
