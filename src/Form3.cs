using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using IWshRuntimeLibrary;

namespace ACC_Kiosk
{
    public partial class Settings : Form
    {
        public object Directory { get; private set; }
        public string defaultDirectory;



        // Get Local IP
        public static string GetClientIpV4()
        {
            IPHostEntry host;
            string localIp = "Unknown";
            string hostName = Dns.GetHostName();
            host = Dns.GetHostEntry(hostName);
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIp = ip.ToString();
                }
            }
            return localIp;
        }


        public Settings()
        {
            InitializeComponent();
        }

        // Make 'Enter' the return key for the form.
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                OKButton.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            // Load Settings
            RoomName.Text = Properties.Settings.Default.roomName;
            confNameText.Text = Properties.Settings.Default.confName;
            PCNameLabel.Text = SystemInformation.ComputerName.ToString();
            textBox1.Text = Properties.Settings.Default.defaultDirectory;
            bgImageText.Text = Properties.Settings.Default.bgImage;

            if(bgImageText.Text.Contains("Adelaide_Convention_Centre"))
            {
                defaultbgButton.Visible = false;
                defaultbgButton.Enabled = false;
            } else { defaultbgButton.Visible = true;  defaultbgButton.Enabled = true; }

            if (RoomName.Text == "" || textBox1.Text =="")
            { OKButton.Enabled = false; shortcutButton.Enabled = false; }

            // get ip
            try { IPLabel.Text = GetClientIpV4(); }
            catch (Exception) { IPLabel.Text = "Unknown"; }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Settings.ActiveForm.Close();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {

            if (RoomName.Text == "")
            {
                OKButton.Enabled = false; shortcutButton.Enabled = false;
                MessageBox.Show("The room/hall name cannot be blank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (confNameText.Text == "")
            {
                OKButton.Enabled = false; shortcutButton.Enabled = false;
                MessageBox.Show("The conference name cannot be blank.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                Properties.Settings.Default.roomName = this.RoomName.Text;
                Properties.Settings.Default.confName = this.confNameText.Text;
                Properties.Settings.Default.settingsVisible = false;
                Properties.Settings.Default.Save();
                Settings.ActiveForm.Close();
            }
        }

        private void RoomName_TextChanged(object sender, EventArgs e)
        {
            // Find whether or not Room Name contains invalid characterss
            string s = RoomName.Text;
            var withoutSpecial = new string(s.Where(c => Char.IsLetterOrDigit(c) || Char.IsWhiteSpace(c)).ToArray());
            if (RoomName.Text != withoutSpecial)
            {
                MessageBox.Show("The hall name '" + RoomName.Text + "' contains characters that are not allowed. Characters that are not allowed include ' \\ / : * ? \" < > | ", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else if (RoomName.Text == "" || textBox1.Text == "")
            { OKButton.Enabled = false; shortcutButton.Enabled = false; }
        }

        private void PCNameLabel_Click(object sender, EventArgs e)
        {

        }

        private void folderSelect_Click(object sender, EventArgs e)
        {
            // Change our default folder
            FolderBrowserDialog folderPicker = new FolderBrowserDialog();

            if(folderPicker.ShowDialog() == DialogResult.Cancel)
            {
                // do nothing
            } else
            {
                try
                {
                    defaultDirectory = folderPicker.SelectedPath.ToString() + "\\";
                    Properties.Settings.Default.defaultDirectory = folderPicker.SelectedPath.ToString() + "\\";
                    textBox1.Text = folderPicker.SelectedPath.ToString() + "\\";
                    Properties.Settings.Default.Save();
                    OKButton.Enabled = true;
                } catch (Exception error)  {
                    MessageBox.Show("Error, invalid folder!\nTry another folder\n" + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if(dialogResult == DialogResult.Yes)
            {
                try {
                    string file = "settings.cfg";
                    if(System.IO.File.Exists(file))
                    {
                        System.IO.File.WriteAllText(file, String.Empty);
                    }
                    MessageBox.Show("Settings & all presentations cleared.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearButton.Enabled = false;

                    // Update our settings
                    Properties.Settings.Default.updateList = true;
                    Properties.Settings.Default.roomName = "";
                    Properties.Settings.Default.confName = "";
                    Properties.Settings.Default.defaultDirectory = @"C:\Users\Public\pres";
                    Properties.Settings.Default.Save();
                    Settings.ActiveForm.Close();
                } catch (Exception error)
                {
                    MessageBox.Show("Error: \n" + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void confNameText_TextChanged(object sender, EventArgs e)
        {
            // Find whether or not Conf Name contains invalid characterss
            string s = confNameText.Text;
            var withoutSpecial = new string(s.Where(c => Char.IsLetterOrDigit(c) || Char.IsWhiteSpace(c)).ToArray());

            if (confNameText.Text != withoutSpecial)
            {
                MessageBox.Show("The conference name '" + RoomName.Text + "' contains characters that are not allowed. Characters that are not allowed include ' \\ / : * ? \" < > | ", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (RoomName.Text == "" || textBox1.Text == "")
            { OKButton.Enabled = false; shortcutButton.Enabled = false; }
            else
            {
                OKButton.Enabled = true; shortcutButton.Enabled = true;
            }

        }

        private void shortcutButton_Click(object sender, EventArgs e)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory).ToString();
            string shortcutLocation = desktop + "\\" + Properties.Settings.Default.roomName.ToString() + ".lnk";
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);
            shortcut.Description = Properties.Settings.Default.roomName.ToString();   // The description of the shortcut
            shortcut.TargetPath = Properties.Settings.Default.defaultDirectory.ToString(); // The path of the file that will launch when the shortcut is run
            shortcut.Save(); // Save the shortcut
            shortcutButton.Enabled = false;
        }

        private void bgSelect_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}"; //my computer
            openFileDialog1.Title = "Locate a new background image";
            openFileDialog1.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG|All Files|*.*";

            DialogResult result = this.openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                OKButton.Enabled = true;
                this.bgImageText.Text = this.openFileDialog1.FileName;
                Properties.Settings.Default.bgImage = openFileDialog1.FileName;
                Properties.Settings.Default.Save();
            } 
         }

        private void bgImageText_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void defaultbgButton_Click(object sender, EventArgs e)
        {
            this.bgImageText.Text = "ACC_Kiosk.Properties.Resources.Adelaide_Convention_Centre";
            Properties.Settings.Default.bgImage = "ACC_Kiosk.Properties.Resources.Adelaide_Convention_Centre";
            Properties.Settings.Default.Save();
        }
    }
}
