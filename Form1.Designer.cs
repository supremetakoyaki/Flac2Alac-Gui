namespace Flac2Alac_Gui
{
    partial class Form1
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
            this.SettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.FfmpegDownloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.DownloadFfmpegButton = new System.Windows.Forms.Button();
            this.BrowseOutputFolder_Button = new System.Windows.Forms.Button();
            this.OutputFolder_TextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BrowseFfmpeg_Button = new System.Windows.Forms.Button();
            this.FfmpegLocation_TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ConversionGroupBox = new System.Windows.Forms.GroupBox();
            this.AddFilesButton = new System.Windows.Forms.Button();
            this.ConvertButton = new System.Windows.Forms.Button();
            this.ClearQueueButton = new System.Windows.Forms.Button();
            this.InputFilesListView = new System.Windows.Forms.ListView();
            this.FilepathHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FfmpegDownloadWorker = new System.ComponentModel.BackgroundWorker();
            this.SettingsGroupBox.SuspendLayout();
            this.ConversionGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // SettingsGroupBox
            // 
            this.SettingsGroupBox.Controls.Add(this.FfmpegDownloadProgressBar);
            this.SettingsGroupBox.Controls.Add(this.DownloadFfmpegButton);
            this.SettingsGroupBox.Controls.Add(this.BrowseOutputFolder_Button);
            this.SettingsGroupBox.Controls.Add(this.OutputFolder_TextBox);
            this.SettingsGroupBox.Controls.Add(this.label2);
            this.SettingsGroupBox.Controls.Add(this.BrowseFfmpeg_Button);
            this.SettingsGroupBox.Controls.Add(this.FfmpegLocation_TextBox);
            this.SettingsGroupBox.Controls.Add(this.label1);
            this.SettingsGroupBox.Location = new System.Drawing.Point(12, 12);
            this.SettingsGroupBox.Name = "SettingsGroupBox";
            this.SettingsGroupBox.Size = new System.Drawing.Size(680, 117);
            this.SettingsGroupBox.TabIndex = 0;
            this.SettingsGroupBox.TabStop = false;
            this.SettingsGroupBox.Text = "Settings";
            // 
            // FfmpegDownloadProgressBar
            // 
            this.FfmpegDownloadProgressBar.Location = new System.Drawing.Point(6, 86);
            this.FfmpegDownloadProgressBar.Name = "FfmpegDownloadProgressBar";
            this.FfmpegDownloadProgressBar.Size = new System.Drawing.Size(472, 25);
            this.FfmpegDownloadProgressBar.TabIndex = 7;
            // 
            // DownloadFfmpegButton
            // 
            this.DownloadFfmpegButton.Location = new System.Drawing.Point(484, 86);
            this.DownloadFfmpegButton.Name = "DownloadFfmpegButton";
            this.DownloadFfmpegButton.Size = new System.Drawing.Size(190, 25);
            this.DownloadFfmpegButton.TabIndex = 6;
            this.DownloadFfmpegButton.Text = "Download ffmpeg";
            this.DownloadFfmpegButton.UseVisualStyleBackColor = true;
            this.DownloadFfmpegButton.Click += new System.EventHandler(this.DownloadFfmpegButton_Click);
            // 
            // BrowseOutputFolder_Button
            // 
            this.BrowseOutputFolder_Button.Location = new System.Drawing.Point(554, 55);
            this.BrowseOutputFolder_Button.Name = "BrowseOutputFolder_Button";
            this.BrowseOutputFolder_Button.Size = new System.Drawing.Size(120, 25);
            this.BrowseOutputFolder_Button.TabIndex = 5;
            this.BrowseOutputFolder_Button.Text = "Browse...";
            this.BrowseOutputFolder_Button.UseVisualStyleBackColor = true;
            this.BrowseOutputFolder_Button.Click += new System.EventHandler(this.BrowseOutputFolder_Button_Click);
            // 
            // OutputFolder_TextBox
            // 
            this.OutputFolder_TextBox.Location = new System.Drawing.Point(115, 55);
            this.OutputFolder_TextBox.Name = "OutputFolder_TextBox";
            this.OutputFolder_TextBox.ReadOnly = true;
            this.OutputFolder_TextBox.Size = new System.Drawing.Size(433, 25);
            this.OutputFolder_TextBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output folder:";
            // 
            // BrowseFfmpeg_Button
            // 
            this.BrowseFfmpeg_Button.Location = new System.Drawing.Point(554, 24);
            this.BrowseFfmpeg_Button.Name = "BrowseFfmpeg_Button";
            this.BrowseFfmpeg_Button.Size = new System.Drawing.Size(120, 25);
            this.BrowseFfmpeg_Button.TabIndex = 2;
            this.BrowseFfmpeg_Button.Text = "Browse...";
            this.BrowseFfmpeg_Button.UseVisualStyleBackColor = true;
            this.BrowseFfmpeg_Button.Click += new System.EventHandler(this.BrowseFfmpeg_Button_Click);
            // 
            // FfmpegLocation_TextBox
            // 
            this.FfmpegLocation_TextBox.Location = new System.Drawing.Point(115, 24);
            this.FfmpegLocation_TextBox.Name = "FfmpegLocation_TextBox";
            this.FfmpegLocation_TextBox.ReadOnly = true;
            this.FfmpegLocation_TextBox.Size = new System.Drawing.Size(433, 25);
            this.FfmpegLocation_TextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "ffmpeg location:";
            // 
            // ConversionGroupBox
            // 
            this.ConversionGroupBox.Controls.Add(this.AddFilesButton);
            this.ConversionGroupBox.Controls.Add(this.ConvertButton);
            this.ConversionGroupBox.Controls.Add(this.ClearQueueButton);
            this.ConversionGroupBox.Controls.Add(this.InputFilesListView);
            this.ConversionGroupBox.Location = new System.Drawing.Point(12, 135);
            this.ConversionGroupBox.Name = "ConversionGroupBox";
            this.ConversionGroupBox.Size = new System.Drawing.Size(680, 414);
            this.ConversionGroupBox.TabIndex = 1;
            this.ConversionGroupBox.TabStop = false;
            this.ConversionGroupBox.Text = "Conversion queue";
            // 
            // AddFilesButton
            // 
            this.AddFilesButton.Location = new System.Drawing.Point(9, 383);
            this.AddFilesButton.Name = "AddFilesButton";
            this.AddFilesButton.Size = new System.Drawing.Size(166, 27);
            this.AddFilesButton.TabIndex = 3;
            this.AddFilesButton.Text = "Add files";
            this.AddFilesButton.UseVisualStyleBackColor = true;
            this.AddFilesButton.Click += new System.EventHandler(this.AddFilesButton_Click);
            // 
            // ConvertButton
            // 
            this.ConvertButton.Location = new System.Drawing.Point(541, 383);
            this.ConvertButton.Name = "ConvertButton";
            this.ConvertButton.Size = new System.Drawing.Size(133, 27);
            this.ConvertButton.TabIndex = 2;
            this.ConvertButton.Text = "Convert!";
            this.ConvertButton.UseVisualStyleBackColor = true;
            this.ConvertButton.Click += new System.EventHandler(this.ConvertButton_Click);
            // 
            // ClearQueueButton
            // 
            this.ClearQueueButton.Location = new System.Drawing.Point(181, 383);
            this.ClearQueueButton.Name = "ClearQueueButton";
            this.ClearQueueButton.Size = new System.Drawing.Size(166, 27);
            this.ClearQueueButton.TabIndex = 1;
            this.ClearQueueButton.Text = "Clear queue";
            this.ClearQueueButton.UseVisualStyleBackColor = true;
            this.ClearQueueButton.Click += new System.EventHandler(this.ClearQueueButton_Click);
            // 
            // InputFilesListView
            // 
            this.InputFilesListView.AllowDrop = true;
            this.InputFilesListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InputFilesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FilepathHeader});
            this.InputFilesListView.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.InputFilesListView.FullRowSelect = true;
            this.InputFilesListView.HideSelection = false;
            this.InputFilesListView.Location = new System.Drawing.Point(9, 30);
            this.InputFilesListView.Name = "InputFilesListView";
            this.InputFilesListView.Size = new System.Drawing.Size(665, 351);
            this.InputFilesListView.TabIndex = 0;
            this.InputFilesListView.UseCompatibleStateImageBehavior = false;
            this.InputFilesListView.View = System.Windows.Forms.View.Details;
            this.InputFilesListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.InputFilesListView_DragDrop);
            this.InputFilesListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.InputFilesListView_DragEnter);
            // 
            // FilepathHeader
            // 
            this.FilepathHeader.Text = "File Path";
            this.FilepathHeader.Width = 660;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(704, 561);
            this.Controls.Add(this.ConversionGroupBox);
            this.Controls.Add(this.SettingsGroupBox);
            this.Font = new System.Drawing.Font("Segoe UI", 9.7F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Flac2Alac GUI";
            this.SettingsGroupBox.ResumeLayout(false);
            this.SettingsGroupBox.PerformLayout();
            this.ConversionGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox SettingsGroupBox;
        private System.Windows.Forms.Button BrowseOutputFolder_Button;
        private System.Windows.Forms.TextBox OutputFolder_TextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BrowseFfmpeg_Button;
        private System.Windows.Forms.TextBox FfmpegLocation_TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox ConversionGroupBox;
        private System.Windows.Forms.Button AddFilesButton;
        private System.Windows.Forms.Button ConvertButton;
        private System.Windows.Forms.Button ClearQueueButton;
        private System.Windows.Forms.ListView InputFilesListView;
        private System.Windows.Forms.ColumnHeader FilepathHeader;
        private System.Windows.Forms.Button DownloadFfmpegButton;
        private System.Windows.Forms.ProgressBar FfmpegDownloadProgressBar;
        private System.ComponentModel.BackgroundWorker FfmpegDownloadWorker;
    }
}

