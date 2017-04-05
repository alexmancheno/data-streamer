﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Collections;
using System.Threading;
using System.Data.SqlClient;

namespace csharprestClient
{

    public partial class GoogleApiCaller : Form
    {
        //properties of the form, which are used to perform the various operations of this same form
        private bool autoWrite = false;
        private Dictionary<string, string> apiDictionary = new Dictionary<string, string>();
        private List<NYSERecord> stockList = new List<NYSERecord>(3500);
        private List<string> stockTickerList = new List<string>(3500);
        private List<NYSERecord> stockQueue = new List<NYSERecord>(3500);

        public GoogleApiCaller()
        {
            InitializeComponent();
            apiDictionary.Add("Google Finance", "https://www.google.com/finance/getprices?i=[PERIOD]&p=[DAYS]d&f=d,o,h,l,c,v&def=cpct&q=[TICKER]");
            apiDictionary.Add("Yahoo Finance", "http://chartapi.finance.yahoo.com/instrument/1.0/[TICKER]/chartdata;type=quote;range=1d/json");
        }

        #region UI Event Handlers

        //this button is currently not used, but I'll fix it a little later
        private void cmdGO_Click(object sender, EventArgs e)
        {
            Regex numsOnly = new Regex("^[0-9]*");

            if (!stockTickerList.Contains(txtTicker.Text))
            {

                //replace substrings with user input from the desktop gui
                string selectedRequest = apiDictionary[comboBoxOptions.Text];
                selectedRequest = selectedRequest.Replace("[TICKER]", txtTicker.Text);
                if (apiDictionary[comboBoxOptions.Text].Contains("[DAYS]"))
                    selectedRequest = selectedRequest.Replace("12000", txtDays.Text);
                if (apiDictionary[comboBoxOptions.Text].Contains("[PERIOD]"))
                    selectedRequest = selectedRequest.Replace("60", txtInterval.Text);

                //create ep and add to queue 
                RestClient rClient = new RestClient();
                rClient.endPoint = selectedRequest;
                string filePath = txtFilePath.Text + txtFileName.Text;

                //stockQueue.Add(txtTicker.Text, new NYSERecord(rClient, filePath));
            } else
            {
                debugOutPut("Stock is already on the records.");
            }
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
                debugOutPut("There was an error selecting the file: " + ex.ToString());
            } 

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            autoWrite = true;
        }

        private void rbNo_CheckedChanged(object sender, EventArgs e)
        {
            autoWrite = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //to remove a record from the 'active' record tab, but it's currently unused because of the Google server
        //problem.
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
                    debugOutPut("There was an error killing the process: " + ex.ToString());
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
                debugOutPut("There was an error selecting the save location: " + ex.ToString());
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
                debugOutPut("There was an error selecting the list file: " + ex.ToString());
            }
        }

        //this is the button that does the heavy lifting: it first adds in all the files into 'stockQueue' 
        private async void btnCreateRecords_Click(object sender, EventArgs e)
        {
            try
            {
                using (StreamReader sr = new StreamReader(txtListFile.Text))
                {
                    string currentLine;
                    char[] seperatingChars = { '\t' };
                    while ((currentLine = sr.ReadLine()) != null)
                    {
                        string[] words = currentLine.Split(seperatingChars, StringSplitOptions.RemoveEmptyEntries);
                        string ep = apiDictionary["Google Finance"];
                        ep = ep.Replace("[PERIOD]", "60");
                        ep = ep.Replace("[TICKER]", words[0]);
                        ep = ep.Replace("[DAYS]", "100000");
                        RestClient rClient = new RestClient();
                        rClient.endPoint = ep;
                        string filePath = txtSaveLocation.Text + words[0] + ".txt";
                        words[0] = words[0].Replace('-', '_');
                        stockQueue.Add(new NYSERecord(rClient, filePath, words[0])); //create new NYSERecord with the current line's 
                        //information, the ticker and company name, which is enough to create the database table and text file
                    }

                    debugOutPut("File was read successfully!");
                    debugOutPut("Starting tasks...");
                    //a thread to handle items 0-1499 (if it's not a null reference) from the 'stockQueue':
                    Task.Run(() =>
                    {
                        Task task1 = Task.Run(() =>
                        {
                            //stockQueue[0].initializeRecord();
                            for (int i = 0; i < 1500 && i < stockQueue.Count; i++)
                            {
                                stockQueue[i].initializeRecord();
                            }
                        });

                        //another thread to handle items 1500 (if it's not a null reference) from the 'stockQueue':
                        //Task task2 = Task.Run(() =>
                        //{
                        //    for (int i = 1500; i < stockQueue.Count && stockQueue[i] != null; i++)
                        //    {
                        //        stockQueue[i].initializeRecord();
                        //    }
                        //});

                        Task.WaitAll(task1); //wait for threads to finish before clearing the stockQueue
                        stockQueue.Clear();
                        debugOutPut("Queue cleared.");
                    });
                }
            }
            catch (Exception ex)
            {
                debugOutPut("There was an error reading the list file: " + ex.ToString());
            }
        }

        private void tabCreateRecord_Click(object sender, EventArgs e)
        {

        }
    }
}