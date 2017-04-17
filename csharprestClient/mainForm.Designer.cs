namespace csharprestClient
{
    partial class GoogleApiCaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabActiveFiles = new System.Windows.Forms.TabPage();
            this.btnRemove = new System.Windows.Forms.Button();
            this.listActiveFiles = new System.Windows.Forms.ListView();
            this.colFilePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabCreateRecord = new System.Windows.Forms.TabPage();
            this.btnSetUpdateTime = new System.Windows.Forms.Button();
            this.txtMinute = new System.Windows.Forms.TextBox();
            this.txtHour = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTicker = new System.Windows.Forms.TextBox();
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.txtDays = new System.Windows.Forms.TextBox();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxOptions = new System.Windows.Forms.ComboBox();
            this.btnCreateRecord = new System.Windows.Forms.Button();
            this.chkStartup = new System.Windows.Forms.CheckBox();
            this.tabCreateFile = new System.Windows.Forms.TabControl();
            this.tabCreateMultipleRecords = new System.Windows.Forms.TabPage();
            this.txtLocationOfFilePaths = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSelectListFile = new System.Windows.Forms.Button();
            this.btnCreateRecords = new System.Windows.Forms.Button();
            this.btnSelectSaveLocation = new System.Windows.Forms.Button();
            this.lblSaveLocation = new System.Windows.Forms.Label();
            this.txtSaveLocation = new System.Windows.Forms.TextBox();
            this.txtListFile = new System.Windows.Forms.TextBox();
            this.lblListFile = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.tabActiveFiles.SuspendLayout();
            this.tabCreateRecord.SuspendLayout();
            this.tabCreateFile.SuspendLayout();
            this.tabCreateMultipleRecords.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tabActiveFiles
            // 
            this.tabActiveFiles.Controls.Add(this.btnRemove);
            this.tabActiveFiles.Controls.Add(this.listActiveFiles);
            this.tabActiveFiles.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tabActiveFiles.Location = new System.Drawing.Point(4, 33);
            this.tabActiveFiles.Name = "tabActiveFiles";
            this.tabActiveFiles.Padding = new System.Windows.Forms.Padding(3);
            this.tabActiveFiles.Size = new System.Drawing.Size(1365, 825);
            this.tabActiveFiles.TabIndex = 1;
            this.tabActiveFiles.Text = "Active Files";
            this.tabActiveFiles.UseVisualStyleBackColor = true;
            // 
            // btnRemove
            // 
            this.btnRemove.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRemove.Location = new System.Drawing.Point(6, 567);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(321, 64);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "Stop auto-writing ";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // listActiveFiles
            // 
            this.listActiveFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listActiveFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFilePath});
            this.listActiveFiles.FullRowSelect = true;
            this.listActiveFiles.Location = new System.Drawing.Point(0, 0);
            this.listActiveFiles.Name = "listActiveFiles";
            this.listActiveFiles.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listActiveFiles.Size = new System.Drawing.Size(1393, 1053);
            this.listActiveFiles.TabIndex = 0;
            this.listActiveFiles.UseCompatibleStateImageBehavior = false;
            this.listActiveFiles.View = System.Windows.Forms.View.Details;
            this.listActiveFiles.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // colFilePath
            // 
            this.colFilePath.Text = "File Path";
            this.colFilePath.Width = 288;
            // 
            // tabCreateRecord
            // 
            this.tabCreateRecord.Controls.Add(this.btnSetUpdateTime);
            this.tabCreateRecord.Controls.Add(this.txtMinute);
            this.tabCreateRecord.Controls.Add(this.txtHour);
            this.tabCreateRecord.Controls.Add(this.label3);
            this.tabCreateRecord.Controls.Add(this.txtTicker);
            this.tabCreateRecord.Controls.Add(this.txtResponse);
            this.tabCreateRecord.Controls.Add(this.txtDays);
            this.tabCreateRecord.Controls.Add(this.txtFilePath);
            this.tabCreateRecord.Controls.Add(this.btnSelectFolder);
            this.tabCreateRecord.Controls.Add(this.label4);
            this.tabCreateRecord.Controls.Add(this.label7);
            this.tabCreateRecord.Controls.Add(this.label6);
            this.tabCreateRecord.Controls.Add(this.label1);
            this.tabCreateRecord.Controls.Add(this.label2);
            this.tabCreateRecord.Controls.Add(this.comboBoxOptions);
            this.tabCreateRecord.Controls.Add(this.btnCreateRecord);
            this.tabCreateRecord.Controls.Add(this.chkStartup);
            this.tabCreateRecord.Location = new System.Drawing.Point(4, 33);
            this.tabCreateRecord.Name = "tabCreateRecord";
            this.tabCreateRecord.Padding = new System.Windows.Forms.Padding(3);
            this.tabCreateRecord.Size = new System.Drawing.Size(1365, 825);
            this.tabCreateRecord.TabIndex = 0;
            this.tabCreateRecord.Text = "Create Record";
            this.tabCreateRecord.UseVisualStyleBackColor = true;
            this.tabCreateRecord.Click += new System.EventHandler(this.tabCreateRecord_Click);
            // 
            // btnSetUpdateTime
            // 
            this.btnSetUpdateTime.Location = new System.Drawing.Point(863, 253);
            this.btnSetUpdateTime.Name = "btnSetUpdateTime";
            this.btnSetUpdateTime.Size = new System.Drawing.Size(418, 60);
            this.btnSetUpdateTime.TabIndex = 21;
            this.btnSetUpdateTime.Text = "Set Update Time";
            this.btnSetUpdateTime.UseVisualStyleBackColor = true;
            this.btnSetUpdateTime.Click += new System.EventHandler(this.btnSetUpdateTime_Click);
            // 
            // txtMinute
            // 
            this.txtMinute.Location = new System.Drawing.Point(1189, 196);
            this.txtMinute.Name = "txtMinute";
            this.txtMinute.Size = new System.Drawing.Size(92, 29);
            this.txtMinute.TabIndex = 20;
            // 
            // txtHour
            // 
            this.txtHour.Location = new System.Drawing.Point(1092, 196);
            this.txtHour.Name = "txtHour";
            this.txtHour.Size = new System.Drawing.Size(92, 29);
            this.txtHour.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(858, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(229, 25);
            this.label3.TabIndex = 18;
            this.label3.Text = "Update time: (24hr-time):";
            // 
            // txtTicker
            // 
            this.txtTicker.Location = new System.Drawing.Point(165, 88);
            this.txtTicker.Name = "txtTicker";
            this.txtTicker.Size = new System.Drawing.Size(369, 29);
            this.txtTicker.TabIndex = 17;
            // 
            // txtResponse
            // 
            this.txtResponse.Location = new System.Drawing.Point(8, 351);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResponse.Size = new System.Drawing.Size(1348, 448);
            this.txtResponse.TabIndex = 1;
            // 
            // txtDays
            // 
            this.txtDays.Location = new System.Drawing.Point(165, 149);
            this.txtDays.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDays.Name = "txtDays";
            this.txtDays.Size = new System.Drawing.Size(369, 29);
            this.txtDays.TabIndex = 8;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(165, 202);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(653, 29);
            this.txtFilePath.TabIndex = 14;
            this.txtFilePath.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(165, 253);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(306, 60);
            this.btnSelectFolder.TabIndex = 16;
            this.btnSelectFolder.Text = "Select Folder\r\n";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(99, 149);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 25);
            this.label4.TabIndex = 9;
            this.label4.Text = "Days:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(67, 202);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 25);
            this.label7.TabIndex = 15;
            this.label7.Text = "File path:";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(90, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 25);
            this.label6.TabIndex = 13;
            this.label6.Text = "Ticker:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(113, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "API:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 324);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Log:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // comboBoxOptions
            // 
            this.comboBoxOptions.FormattingEnabled = true;
            this.comboBoxOptions.Items.AddRange(new object[] {
            "Google Finance",
            "Yahoo Finance"});
            this.comboBoxOptions.Location = new System.Drawing.Point(165, 35);
            this.comboBoxOptions.Name = "comboBoxOptions";
            this.comboBoxOptions.Size = new System.Drawing.Size(369, 32);
            this.comboBoxOptions.TabIndex = 12;
            // 
            // btnCreateRecord
            // 
            this.btnCreateRecord.Location = new System.Drawing.Point(490, 253);
            this.btnCreateRecord.Name = "btnCreateRecord";
            this.btnCreateRecord.Size = new System.Drawing.Size(328, 60);
            this.btnCreateRecord.TabIndex = 2;
            this.btnCreateRecord.Text = "Create Record";
            this.btnCreateRecord.UseVisualStyleBackColor = true;
            this.btnCreateRecord.Click += new System.EventHandler(this.cmdGO_Click);
            // 
            // chkStartup
            // 
            this.chkStartup.AutoSize = true;
            this.chkStartup.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkStartup.Location = new System.Drawing.Point(863, 36);
            this.chkStartup.Name = "chkStartup";
            this.chkStartup.Size = new System.Drawing.Size(232, 29);
            this.chkStartup.TabIndex = 23;
            this.chkStartup.Text = "Startup with Windows:";
            this.chkStartup.UseVisualStyleBackColor = true;
            this.chkStartup.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // tabCreateFile
            // 
            this.tabCreateFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCreateFile.Controls.Add(this.tabCreateRecord);
            this.tabCreateFile.Controls.Add(this.tabCreateMultipleRecords);
            this.tabCreateFile.Controls.Add(this.tabActiveFiles);
            this.tabCreateFile.Location = new System.Drawing.Point(13, 2);
            this.tabCreateFile.Name = "tabCreateFile";
            this.tabCreateFile.SelectedIndex = 0;
            this.tabCreateFile.Size = new System.Drawing.Size(1373, 862);
            this.tabCreateFile.TabIndex = 17;
            // 
            // tabCreateMultipleRecords
            // 
            this.tabCreateMultipleRecords.Controls.Add(this.txtLocationOfFilePaths);
            this.tabCreateMultipleRecords.Controls.Add(this.button1);
            this.tabCreateMultipleRecords.Controls.Add(this.label8);
            this.tabCreateMultipleRecords.Controls.Add(this.btnSelectListFile);
            this.tabCreateMultipleRecords.Controls.Add(this.btnCreateRecords);
            this.tabCreateMultipleRecords.Controls.Add(this.btnSelectSaveLocation);
            this.tabCreateMultipleRecords.Controls.Add(this.lblSaveLocation);
            this.tabCreateMultipleRecords.Controls.Add(this.txtSaveLocation);
            this.tabCreateMultipleRecords.Controls.Add(this.txtListFile);
            this.tabCreateMultipleRecords.Controls.Add(this.lblListFile);
            this.tabCreateMultipleRecords.Location = new System.Drawing.Point(4, 33);
            this.tabCreateMultipleRecords.Name = "tabCreateMultipleRecords";
            this.tabCreateMultipleRecords.Size = new System.Drawing.Size(1365, 825);
            this.tabCreateMultipleRecords.TabIndex = 2;
            this.tabCreateMultipleRecords.Text = "Create Multiple Records";
            this.tabCreateMultipleRecords.UseVisualStyleBackColor = true;
            // 
            // txtLocationOfFilePaths
            // 
            this.txtLocationOfFilePaths.Location = new System.Drawing.Point(235, 333);
            this.txtLocationOfFilePaths.Name = "txtLocationOfFilePaths";
            this.txtLocationOfFilePaths.Size = new System.Drawing.Size(1037, 29);
            this.txtLocationOfFilePaths.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(235, 386);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(330, 51);
            this.button1.TabIndex = 11;
            this.button1.Text = "Select File Path Save Location";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 333);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(195, 25);
            this.label8.TabIndex = 10;
            this.label8.Text = "Location of file paths:";
            // 
            // btnSelectListFile
            // 
            this.btnSelectListFile.Location = new System.Drawing.Point(235, 212);
            this.btnSelectListFile.Name = "btnSelectListFile";
            this.btnSelectListFile.Size = new System.Drawing.Size(330, 49);
            this.btnSelectListFile.TabIndex = 8;
            this.btnSelectListFile.Text = "Select List File";
            this.btnSelectListFile.UseVisualStyleBackColor = true;
            this.btnSelectListFile.Click += new System.EventHandler(this.btnSelectListFile_Click);
            // 
            // btnCreateRecords
            // 
            this.btnCreateRecords.Location = new System.Drawing.Point(950, 212);
            this.btnCreateRecords.Name = "btnCreateRecords";
            this.btnCreateRecords.Size = new System.Drawing.Size(322, 49);
            this.btnCreateRecords.TabIndex = 5;
            this.btnCreateRecords.Text = "Create Records";
            this.btnCreateRecords.UseVisualStyleBackColor = true;
            this.btnCreateRecords.Click += new System.EventHandler(this.btnCreateRecords_Click);
            // 
            // btnSelectSaveLocation
            // 
            this.btnSelectSaveLocation.Location = new System.Drawing.Point(578, 212);
            this.btnSelectSaveLocation.Name = "btnSelectSaveLocation";
            this.btnSelectSaveLocation.Size = new System.Drawing.Size(341, 49);
            this.btnSelectSaveLocation.TabIndex = 4;
            this.btnSelectSaveLocation.Text = "Select Save Location";
            this.btnSelectSaveLocation.UseVisualStyleBackColor = true;
            this.btnSelectSaveLocation.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // lblSaveLocation
            // 
            this.lblSaveLocation.AutoSize = true;
            this.lblSaveLocation.Location = new System.Drawing.Point(87, 160);
            this.lblSaveLocation.Name = "lblSaveLocation";
            this.lblSaveLocation.Size = new System.Drawing.Size(143, 25);
            this.lblSaveLocation.TabIndex = 3;
            this.lblSaveLocation.Text = "Save Location:";
            // 
            // txtSaveLocation
            // 
            this.txtSaveLocation.Location = new System.Drawing.Point(235, 160);
            this.txtSaveLocation.Name = "txtSaveLocation";
            this.txtSaveLocation.Size = new System.Drawing.Size(1037, 29);
            this.txtSaveLocation.TabIndex = 2;
            // 
            // txtListFile
            // 
            this.txtListFile.Location = new System.Drawing.Point(235, 110);
            this.txtListFile.Name = "txtListFile";
            this.txtListFile.Size = new System.Drawing.Size(1037, 29);
            this.txtListFile.TabIndex = 1;
            this.txtListFile.TextChanged += new System.EventHandler(this.txtListFile_TextChanged);
            // 
            // lblListFile
            // 
            this.lblListFile.AutoSize = true;
            this.lblListFile.Location = new System.Drawing.Point(144, 110);
            this.lblListFile.Name = "lblListFile";
            this.lblListFile.Size = new System.Drawing.Size(84, 25);
            this.lblListFile.TabIndex = 0;
            this.lblListFile.Text = "List File:";
            this.lblListFile.Click += new System.EventHandler(this.lblListFilePath_Click);
            // 
            // GoogleApiCaller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1397, 876);
            this.Controls.Add(this.tabCreateFile);
            this.Name = "GoogleApiCaller";
            this.Text = "Numeraxial Data Stream ";
            this.Load += new System.EventHandler(this.GoogleApiCaller_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.tabActiveFiles.ResumeLayout(false);
            this.tabCreateRecord.ResumeLayout(false);
            this.tabCreateRecord.PerformLayout();
            this.tabCreateFile.ResumeLayout(false);
            this.tabCreateMultipleRecords.ResumeLayout(false);
            this.tabCreateMultipleRecords.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        private System.Windows.Forms.TabPage tabActiveFiles;
        private System.Windows.Forms.TabPage tabCreateRecord;
        private System.Windows.Forms.TextBox txtTicker;
        private System.Windows.Forms.TextBox txtResponse;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxOptions;
        private System.Windows.Forms.Button btnCreateRecord;
        private System.Windows.Forms.TabControl tabCreateFile;
        private System.Windows.Forms.ListView listActiveFiles;
        private System.Windows.Forms.ColumnHeader colFilePath;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.TabPage tabCreateMultipleRecords;
        private System.Windows.Forms.Label lblSaveLocation;
        private System.Windows.Forms.TextBox txtSaveLocation;
        private System.Windows.Forms.TextBox txtListFile;
        private System.Windows.Forms.Label lblListFile;
        private System.Windows.Forms.Button btnSelectSaveLocation;
        private System.Windows.Forms.Button btnCreateRecords;
        private System.Windows.Forms.Button btnSelectListFile;
        private System.Windows.Forms.TextBox txtDays;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSetUpdateTime;
        private System.Windows.Forms.TextBox txtMinute;
        private System.Windows.Forms.TextBox txtHour;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLocationOfFilePaths;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkStartup;
    }
}

