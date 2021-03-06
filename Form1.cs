using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flac2Alac_Gui
{
    public partial class Form1 : Form
    {
        public string FfmpegPath
        {
            get => FfmpegLocation_TextBox.Text;
            set => FfmpegLocation_TextBox.Text = value;
        }

        public string OutputDirectoryPath
        {
            get => OutputFolder_TextBox.Text;
            set => OutputFolder_TextBox.Text = value;
        }

        public Form1()
        {
            InitializeComponent();
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            TryLoadSettings();
            PrepareBackgroundWorker();
        }

        public void TryLoadSettings()
        {
            try
            {
                string[] SettingsString = File.ReadAllText("flac2alacgui.settings").Split(Environment.NewLine.ToCharArray());
                for (int i = 0; i < SettingsString.Length; i++)
                {
                    string[] Setting = SettingsString[i].Split('=');
                    if (Setting[0] == "ffmpegpath")
                    {
                        FfmpegPath = Setting[1];
                    }
                    else if (Setting[0] == "outputdir")
                    {
                        OutputDirectoryPath = Setting[1];
                    }
                }
            }
            catch
            {
                FfmpegPath = "";
                OutputDirectoryPath = Environment.CurrentDirectory + "\\Converted";
                Directory.CreateDirectory("Converted");
                SaveSettings();
            }
        }

        public void PrepareBackgroundWorker()
        {
            FfmpegDownloadWorker.DoWork += FfmpegDownloadWorker_DoWork;
            FfmpegDownloadWorker.ProgressChanged += FfmpegDownloadWorker_ProgressChanged;
            FfmpegDownloadWorker.WorkerReportsProgress = true;
            FfmpegDownloadWorker.RunWorkerCompleted += FfmpegDownloadWorker_RunWorkerCompleted;
        }

        public string GenerateSettingsString()
        {
            StringBuilder Settings = new StringBuilder();
            Settings.Append("ffmpegpath=");
            Settings.AppendLine(FfmpegPath);
            Settings.Append("outputdir=");
            Settings.AppendLine(OutputDirectoryPath);
            return Settings.ToString();
        }

        public void SaveSettings()
        {
            File.WriteAllText("flac2alacgui.settings", GenerateSettingsString());
        }

        private void BrowseFfmpeg_Button_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog Dialog = new OpenFileDialog())
            {
                Dialog.FileName = "ffmpeg.exe";
                Dialog.Filter = "ffmpeg binary|ffmpeg.exe";
                Dialog.InitialDirectory = "C:\\";

                if (Dialog.ShowDialog() == DialogResult.OK)
                {
                    FfmpegPath = Dialog.FileName;
                    SaveSettings();
                }
            }
        }

        private void BrowseOutputFolder_Button_Click(object sender, EventArgs e)
        {
            using (CommonOpenFileDialog Dialog = new CommonOpenFileDialog())
            {
                Dialog.IsFolderPicker = true;

                if (Directory.Exists(OutputDirectoryPath))
                {
                    Dialog.InitialDirectory = OutputDirectoryPath;
                }
                else if (Directory.Exists("Converted"))
                {
                    Dialog.InitialDirectory = Environment.CurrentDirectory + "\\Converted";
                }
                else
                {
                    Dialog.InitialDirectory = Environment.CurrentDirectory;
                }

                if (Dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    OutputDirectoryPath = Dialog.FileName;
                    SaveSettings();
                }
            }
        }

        private bool Contains(string FileName)
        {
            foreach (ListViewItem Item in InputFilesListView.Items)
            {
                if (Item.Text == FileName)
                {
                    return true;
                }
            }

            return false;
        }

        private void AddFilesButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog Dialog = new OpenFileDialog())
            {
                Dialog.Filter = "Supported lossless audio files|*.flac;*.wav|All files (*.*)|*.*";
                Dialog.Multiselect = true;

                if (Dialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string FileName in Dialog.FileNames)
                    {
                        if (!Contains(FileName))
                        {
                            if (!FileName.EndsWith(".flac") && !FileName.EndsWith(".wav"))
                            {
                                if (MessageBox.Show(string.Format("{0}\n\nThis file doesn't have a FLAC or WAV extension. Do you still want to add it?", FileName), "Notice", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    InputFilesListView.Items.Add(new ListViewItem(FileName));
                                }
                            }
                            else
                            {
                                InputFilesListView.Items.Add(new ListViewItem(FileName));
                            }
                        }
                    }
                }
            }
        }

        private void ClearQueueButton_Click(object sender, EventArgs e)
        {
            InputFilesListView.Items.Clear();
        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            if (FfmpegDownloadWorker.IsBusy)
            {
                MessageBox.Show("Let's wait until ffmpeg finishes downloading.", "Oops");
                return;
            }
            else if (!File.Exists(FfmpegPath))
            {
                MessageBox.Show("Invalid path for ffmpeg.exe", "Oops");
                return;
            }

            Task.Run(BeginConversion);
        }

        private void BeginConversion()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    ConvertButton.Enabled = false;
                    DownloadFfmpegButton.Enabled = false;
                    BrowseFfmpeg_Button.Enabled = false;
                    BrowseOutputFolder_Button.Enabled = false;
                    AddFilesButton.Enabled = false;
                    ClearQueueButton.Enabled = false;
                    GeneralProgressBar.Value = 0;
                }
                ));
            }
            else
            {
                ConvertButton.Enabled = false;
                DownloadFfmpegButton.Enabled = false;
                BrowseFfmpeg_Button.Enabled = false;
                BrowseOutputFolder_Button.Enabled = false;
                AddFilesButton.Enabled = false;
                ClearQueueButton.Enabled = false;
                GeneralProgressBar.Value = 0;
            }

            double Progress = 0;
            double Increment = InputFilesListView.Items.Count == 0 ? 0 : (double)100 / InputFilesListView.Items.Count;

            while (InputFilesListView.Items.Count > 0)
            {
                string InputFileName = "";

                if (InvokeRequired)
                {
                    Invoke(new Action(() => { InputFileName = InputFilesListView.Items[0].Text; }));
                }
                else
                {
                    InputFileName = InputFilesListView.Items[0].Text;
                }

                RunConvertProcess(InputFileName);

                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        InputFilesListView.Items.RemoveAt(0);

                        if (Progress + Increment > 99)
                        {
                            Progress = 99;
                        }
                        else
                        {
                            Progress = Progress + Increment;
                        }

                        GeneralProgressBar.Value = (int)Progress;
                    }
                    ));
                }
                else
                {
                    InputFilesListView.Items.RemoveAt(0);

                    if (Progress + Increment > 99)
                    {
                        Progress = 100;
                    }
                    else
                    {
                        Progress = Progress + Increment;
                    }

                    GeneralProgressBar.Value = (int)Progress;
                }
            }

            MessageBox.Show("Finished!", "Notice");

            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    ConvertButton.Enabled = true;
                    DownloadFfmpegButton.Enabled = true;
                    BrowseFfmpeg_Button.Enabled = true;
                    BrowseOutputFolder_Button.Enabled = true;
                    AddFilesButton.Enabled = true;
                    ClearQueueButton.Enabled = true;
                    GeneralProgressBar.Value = 100;
                }
                ));
            }
            else
            {
                ConvertButton.Enabled = true;
                DownloadFfmpegButton.Enabled = true;
                BrowseFfmpeg_Button.Enabled = true;
                BrowseOutputFolder_Button.Enabled = true;
                AddFilesButton.Enabled = true;
                ClearQueueButton.Enabled = true;
                GeneralProgressBar.Value = 100;
            }
        }

        private void RunConvertProcess(string InputFileName)
        {
            string OutputFileName = string.Format("{0}\\{1}.m4a", OutputDirectoryPath, Path.GetFileNameWithoutExtension(InputFileName));

            ProcessStartInfo StartInfo = new ProcessStartInfo
            {
                FileName = FfmpegPath,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = Path.GetDirectoryName(FfmpegPath),
                RedirectStandardOutput = true,
                Arguments = string.Format("-i \"{0}\" -vn -acodec alac \"{1}\"", InputFileName, OutputFileName)
            };

            Process Proc = Process.Start(StartInfo);
            Proc.WaitForExit();
        }

        private void DownloadFfmpegButton_Click(object sender, EventArgs e)
        {
            DownloadFfmpegButton.Enabled = false;
            GeneralProgressBar.Value = 0;
            FfmpegDownloadWorker.RunWorkerAsync();
        }

        private void FfmpegDownloadWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Uri Url = new Uri("https://www.gyan.dev/ffmpeg/builds/ffmpeg-release-essentials.zip");
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(Url);
                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
                Response.Close();

                long Size = Response.ContentLength;
                long RunningByteTotal = 0;

                using (WebClient Client = new WebClient())
                {
                    using (Stream StreamRemote = Client.OpenRead(Url))
                    {
                        using (Stream StreamLocal = new FileStream("ffmpeg-release-essentials.zip", FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            int ByteSize = 0;
                            byte[] ByteBuffer = new byte[Size];

                            while ((ByteSize = StreamRemote.Read(ByteBuffer, 0, ByteBuffer.Length)) > 0)
                            {
                                StreamLocal.Write(ByteBuffer, 0, ByteSize);
                                RunningByteTotal += ByteSize;

                                FfmpegDownloadWorker.ReportProgress((int)((double)RunningByteTotal / ByteBuffer.Length * 100));
                            }

                            StreamLocal.Close();
                        }

                        StreamRemote.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error...\n" + ex.ToString(), "Oops");
            }
        }

        private void FfmpegDownloadWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            GeneralProgressBar.Value = e.ProgressPercentage;
        }

        private void FfmpegDownloadWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GeneralProgressBar.Value = 100;
            DownloadFfmpegButton.Enabled = true;

            using (ZipArchive Archive = ZipFile.OpenRead("ffmpeg-release-essentials.zip"))
            {
                ZipArchiveEntry Entry = Archive.Entries.FirstOrDefault(z => z.Name == "ffmpeg.exe");

                if (Entry == null)
                {
                    MessageBox.Show("Error! I cannot extract ffmpeg.exe from the .zip file (it doesn't exist?) Try doing it yourself.", "Oops");
                    return;
                }

                Entry.ExtractToFile(Entry.Name);
            }

            File.Delete("ffmpeg-release-essentials.zip");
            FfmpegPath = Environment.CurrentDirectory + "\\ffmpeg.exe";

            MessageBox.Show("Finished downloading ffmpeg. You can now proceed to convert.", "Notice");
            GeneralProgressBar.Value = 0;
            SaveSettings();
        }

        private void InputFilesListView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void InputFilesListView_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            foreach (string File in FileList)
            {
                if (!Contains(File))
                {
                    if (!File.EndsWith(".flac") && !File.EndsWith(".wav"))
                    {
                        if (MessageBox.Show(string.Format("{0}\n\nThis file doesn't have a FLAC or WAV extension. Do you still want to add it?", File), "Notice", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            InputFilesListView.Items.Add(new ListViewItem(File));
                        }
                    }
                    else
                    {
                        InputFilesListView.Items.Add(new ListViewItem(File));
                    }
                }
            }
        }

        private void DeleteSelectedFile()
        {
            if (InputFilesListView.SelectedItems.Count == 1)
            {
                int Index = InputFilesListView.SelectedItems[0].Index;
                InputFilesListView.Items.RemoveAt(Index);

                if (InputFilesListView.Items.Count > Index)
                {
                    InputFilesListView.SelectedIndices.Add(Index);
                    Select();
                }
                else if (Index != 0 && InputFilesListView.Items.Count > Index - 1)
                {
                    InputFilesListView.SelectedIndices.Add(Index - 1);
                    Select();
                }
            }
        }

        private void InputFilesListView_DoubleClick(object sender, EventArgs e)
        {
            DeleteSelectedFile();
        }

        private void InputFilesListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedFile();
            }
        }
    }
}