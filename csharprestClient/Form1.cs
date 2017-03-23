using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace csharprestClient
{

    public partial class GoogleApiCaller : Form
    {
        private bool autoWrite = false;
        //private Dictionary<string, autoWriter> threadDictionary = new Dictionary<string, autoWriter>();
        //dictionary which holds the available API's
        public Dictionary<string, string> apiDictionary = new Dictionary<string, string>();
        //apiDictionary.
        private Dictionary<string, NYSERecord> stockDictionary = new Dictionary<string, NYSERecord>(3000);
        private Dictionary<string, NYSERecord> stockQueue = new Dictionary<string, NYSERecord>(3000);

        class recordUpdater
        {
            private Dictionary<string, NYSERecord> s;
            private Dictionary<string, NYSERecord> q;

            public recordUpdater(Dictionary<string, NYSERecord> sd, Dictionary<string, NYSERecord> sq)
            {
                s = sd;
                q = sq;
            }
            
            public void update(string s)
            {
                Console.WriteLine(s);
            }

        }


        public GoogleApiCaller()
        {
            InitializeComponent();
            apiDictionary.Add("Google Finance", "https://www.google.com/finance/getprices?i=[PERIOD]&p=[DAYS]d&f=d,o,h,l,c,v&def=cpct&q=[TICKER]");
            apiDictionary.Add("Yahoo Finance", "http://chartapi.finance.yahoo.com/instrument/1.0/[TICKER]/chartdata;type=quote;range=1d/json");
            ParameterizedThreadStart pts = new ParameterizedThreadStart();
        }

        #region UI Event Handlers

        private void cmdGO_Click(object sender, EventArgs e)
        {
            Regex numsOnly = new Regex("^[0-9]*");


            if (!stockDictionary.ContainsKey(txtTicker.Text))
            {

                //replace substrings with user input from the desktop gui
                string selectedRequest = apiDictionary[comboBoxOptions.Text];
                selectedRequest = selectedRequest.Replace("[TICKER]", txtTicker.Text);
                if (apiDictionary[comboBoxOptions.Text].Contains("[DAYS]"))
                    selectedRequest = selectedRequest.Replace("12000", txtDays.Text);
                if (apiDictionary[comboBoxOptions.Text].Contains("[PERIOD]"))
                    selectedRequest = selectedRequest.Replace("60", txtInterval.Text);

                //create endpoint and add to queue 
                RestClient rClient = new RestClient();
                rClient.endPoint = selectedRequest;
                string filePath = txtFilePath.Text + txtFileName.Text;

                stockQueue.Add(txtTicker.Text, new NYSERecord(rClient, filePath));
            } else
            {
                debugOutPut("Stock is already on the records.");
            }
        }


        #endregion

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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.RootFolder = Environment.SpecialFolder.Desktop;
                fbd.Description = "Select a folder where the *.txt file will be saved";
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    txtFilePath.Text = fbd.SelectedPath + "\\";
            } catch (Exception ex)
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

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listActiveFiles.Items.Count > 0)
            {
                try
                {
                    ListViewItem item = listActiveFiles.SelectedItems[0];
                    string fileName = item.SubItems[0].Text;
                    debugOutPut(fileName);
                    //threadDictionary[fileName].stopUpdating();
                    //threadDictionary.Remove(fileName);
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
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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

        private void btnCreateRecords_Click(object sender, EventArgs e)
        {

        }
    }
}
