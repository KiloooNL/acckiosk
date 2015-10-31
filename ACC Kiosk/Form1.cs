using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Net;

namespace ACC_Kiosk
{
    public partial class Kiosk : Form
    {
        private PPTBrowser pptForm = new PPTBrowser();
        private System.Windows.Forms.Timer timer1;
        public string programtype;
        public string openPPTFile;

        public class PPT
        {
            public string Time;
            public string Day;
            public string Name;
            public string Room;
            public string Path;
            public string File;
            public string Type;

            public PPT()
            {

            }


        }

        public void InitTimer()
        {
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 500; // in miliseconds
            timer1.Start();
        }
        // Initialize ListView
        private void CreateSettingsCFG()
        {
            if(!System.IO.File.Exists(@"settings.cfg"))
            {
                string settingscfg = @"settings.cfg";
                Stream stream = null;
                try
                {
                    stream = new FileStream(settingscfg, FileMode.Create);
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        stream = null;
                        writer.WriteLine("");
                        writer.Close();
                    }
                }
                catch(Exception er)
                {
                    DialogResult dialogresult = MessageBox.Show("Error creating settings file, is the directory read-only?\nError details: \n" + er.ToString(), "Fatal Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (dialogresult == DialogResult.Retry)
                    {
                        CreateSettingsCFG();
                    }
                    else
                    {
                        this.Close(); // quit
                    }
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Dispose();
                    }
                }


            }
            /*if (!System.IO.File.Exists(@"settings.cfg"))
            {
                File.Create(settingscfg).Dispose();
                try
                {
                    using (TextWriter tw = new StreamWriter(settingscfg))
                    {
                        tw.WriteLine("");
                        tw.Close();
                    }
                }
                catch (Exception er)
                {
                    DialogResult dialogresult = MessageBox.Show("Error creating settings file, is the directory read-only?\nError details: \n" + er.ToString(), "Fatal Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if(dialogresult == DialogResult.Retry) {
                        CreateSettingsCFG();
                    } else
                    {
                        this.Close(); // quit
                    }
                }
            }*/
        }

        private void InitializeListView()
        {
            // Set the view to show details.
            PresList.View = View.Details;

            // Allow the user to edit item text.
            PresList.LabelEdit = true;

            // Allow the user to rearrange columns.
            PresList.AllowColumnReorder = false;

            // Select the item and subitems when selection is made.
            PresList.FullRowSelect = true;

            // Display grid lines.
            PresList.GridLines = true;

            // Sort the items in the list in ascending order.
            PresList.Sorting = SortOrder.Descending;

            // Attach Subitems to the ListView
            PresList.Columns.Add("Time", 60, HorizontalAlignment.Left);
            PresList.Columns.Add("Day", 70, HorizontalAlignment.Left);
            PresList.Columns.Add("Name", 150, HorizontalAlignment.Left);
            PresList.Columns.Add("Room", 100, HorizontalAlignment.Left);
            PresList.Columns.Add("File", 0, HorizontalAlignment.Left);
            PresList.Columns.Add("Type", 90, HorizontalAlignment.Left);
            
            PresList.ColumnWidthChanging += PresList_ColumnWidthChanging;
        }

        // Prevent Column Resizing
        private void PresList_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == 0 || e.ColumnIndex == 1) { e.NewWidth = 60;  e.Cancel = true; }
            else if (e.ColumnIndex == 2) { e.NewWidth = 150; e.Cancel = true; }
            else if (e.ColumnIndex == 3 || e.ColumnIndex == 5) { e.NewWidth = 100; e.Cancel = true; }
            else if (e.ColumnIndex == 4) { e.NewWidth = 0; e.Cancel = true; }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            // If settings have been changed, make the option invisible
            if (Properties.Settings.Default.settingsVisible == false)
            {
                //SettingsButton.Visible = false; //disable for debugging
            }

            // If clear settings is pressed, we update the list to reflect that.
            if (Properties.Settings.Default.updateList == true)
            {
                PresList.Clear();
                InitializeListView();
                Properties.Settings.Default.updateList = false;
                Properties.Settings.Default.Save();

            }
            try
            {
                //CreateSettingsCFG(); //create settings CFG if doesnt exist
                using (FileStream fs = new FileStream(@"settings.cfg", FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
                    {
                        string line = null;
                        // fix this
                        // PresList.Items.Clear();

                        /*
                        ^        Start of string
                        \d+      "\d" means "digit" - 0-9. The "+" means "one or more."
                                 So this means "one or more digits."
                        \t       This matches a tab.
                        (\d+)    This also matches one or more digits. This time, though, we capture it
                                 using brackets. This means we can access it using the Group method.
                        \t       Another tab.
                        .+?      "." means "anything." So "one or more of anything". In addition, it's lazy.
                                 This is to stop it grabbing everything in sight - it'll only grab as much
                                 as it needs to for the regex to work.
                        \t       Another tab.

                        (item\\[^\t]+\.ddj)
                            Here's the meat. This matches: "item\<one or more of anything but a tab>.ddj"*/

                        while ((line = reader.ReadLine()) != null)
                        {

                            PPT Pres = new PPT();
                            Pres.Time = reader.ReadLine();
                            Pres.Day = reader.ReadLine();
                            Pres.Name = reader.ReadLine();
                            Pres.Room = reader.ReadLine();
                            Pres.File = reader.ReadLine();
                            Pres.Type = reader.ReadLine();

                            // Problem here is that Pres.File is actually original file location (USB for example, so we need to change that to the new file)
                            Pres.File = Pres.Type;

                            if (Pres.Type=="")
                            {
                                Pres.Type = "PowerPoint"; programtype = "ppt";
                            }
                            // Add Pres to ArrayList
                            List<PPT> presArray = new List<PPT>();

                            presArray.Add(Pres);

                            // Sort file types
                            if (Pres.Type.EndsWith(".pdf")) { Pres.Type = "Adobe Reader"; programtype = "adobe"; }
                            if (Pres.Type.EndsWith(".ppt") || Pres.Type.EndsWith(".pptx")) { Pres.Type = "PowerPoint"; programtype = "ppt"; }
                            if (Pres.Type.EndsWith("Prezi.exe")) { Pres.Type = "Prezi"; programtype = "prezi"; }
                            if (Pres.Type.EndsWith(".exe")) { Pres.Type = "Executable"; programtype = "exe"; }
                            if (Pres.Type.EndsWith(".zip") || Pres.Type.EndsWith(".rar")) { Pres.Type = "Archive"; programtype = "exe"; }
                            if (Pres.Type.EndsWith(".txt") || Pres.Type.EndsWith(".text")) { Pres.Type = "Text Document"; programtype = "text"; }
                            if (Pres.Type.EndsWith(".doc") || Pres.Type.EndsWith(".docx")) { Pres.Type = "Word Document"; programtype = "word"; }
                            if (Pres.Type.EndsWith(".html") || Pres.Type.EndsWith(".htm")) { Pres.Type = "HTML"; programtype = "html";  }

                            // assuming you had a pre-existing item
                            ListViewItem item = PresList.FindItemWithText(Pres.File);
                            
                            if (item == null)
                            {
                                foreach (PPT pre in presArray)
                                {
                                    string[] row = { Pres.Time, Pres.Day, Pres.Name, Pres.Room, Pres.File, Pres.Type };
                                    var listViewItem = new ListViewItem(row);
                                    PresList.Items.Add(listViewItem);
                                }
                            } else { }
                        }
                    }
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("There was an error updating the list: \n" + error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Clock.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }
        public Kiosk()
        {
            InitializeComponent();
        }

        // if CTRL + ALT + END are pressed, exit
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Insert)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void AddPres_Click(object sender, EventArgs e)
        {
            PPTBrowser frm = new PPTBrowser();
            frm.Show();
        }

        private void PresList_DoubleClick(object sender, MouseEventArgs e)
        {
            openPPTFile = PresList.SelectedItems[0].SubItems[4].Text;
            StartPresButton.PerformClick();
        }
        private void PresList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PresList.SelectedItems.Count == 0)
            {

            }
            else
            openPPTFile = PresList.SelectedItems[0].SubItems[4].Text;
        }

        private void getPPTFiles()
        {
            PresList.Items.Add(pptForm.pptFileName.Text);
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            label2.Visible = false;
            Settings frm = new Settings();
            frm.Show();
        }
        private void Kiosk_Load(object sender, EventArgs e)
        {

            Clock.Text = DateTime.Now.ToString("hh:mm:ss tt");
            Properties.Settings.Default.settingsVisible = true;
            Properties.Settings.Default.Save();
            SettingsButton.Visible = true;
            
            // Maximize Window
            // FormBorderStyle = FormBorderStyle.None;
            // WindowState = FormWindowState.Maximized;
            timer2 = new System.Windows.Forms.Timer();
            timer2.Tick += new EventHandler(timer2_Tick);
            timer2.Interval = 1000; // in miliseconds
            timer2.Start();

            Blink();
            InitializeListView();
            PresList.MouseDoubleClick += new MouseEventHandler(PresList_DoubleClick);
            InitTimer();
        }
        private async void Blink()
        {
            if (Properties.Settings.Default.confName == "")
            {

            }
            else {
                label2.Text = "Check conference & hall/room name in settings --->";
                while (true)
                {
                    await Task.Delay(500);
                    label2.ForeColor = label2.ForeColor == Color.Red ? Color.Black : Color.Red;
                } 
            }
        }

        private void StartPresButton_Click(object sender, EventArgs e)
        {
            if (openPPTFile == "")
            {
                StartPresButton.Enabled = false;
            }
            else
            {
                try {
                    openPPT(openPPTFile, "/s");
                } catch (Exception eventargs)
                {
                    MessageBox.Show("Error: " + eventargs, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } 
        }

        void openPPT(string file, string args)
        {
            string pptdir = @"";
            if (programtype == "ppt")
            {
                string dir = "";

                try
                {
                    RegistryKey key = Registry.LocalMachine;
                    RegistryKey pptKey = key.OpenSubKey(@"SOFTWARE\Microsoft\Office");
                    if (pptKey != null)
                    {
                        foreach (string valuename in pptKey.GetSubKeyNames())
                        {
                            int version = 9;
                            double currentVersion = 0;
                            if (Double.TryParse(valuename, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out currentVersion) && currentVersion >= version)
                            {
                                RegistryKey rootdir = pptKey.OpenSubKey(currentVersion + @".0\PowerPoint\InstallRoot");
                                if (rootdir != null)
                                {
                                    if (Double.TryParse(valuename, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out currentVersion) && currentVersion >= version)
                                        dir = rootdir.GetValue(rootdir.GetValueNames()[0]).ToString();
                                    //break;
                                }
                            }
                        }
                    }
                }
                catch (Exception ef)
                {
                    MessageBox.Show("Error: " + ef, "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                pptdir = dir + "POWERPNT.exe";
                // pptdir = @"C:\Program Files (x86)\Microsoft Office\Office14\POWERPNT.exe";
            }
            else if (programtype == "adobe") // get adobe settings
            {
                var adobe = Registry.LocalMachine.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows").OpenSubKey("CurrentVersion").OpenSubKey("App Paths").OpenSubKey("AcroRd32.exe");
                var path = adobe.GetValue("");
                pptdir = path.ToString();
                args = "/A \"pagemode = FullScreen\" ";
            }
            else if (programtype == "prezi") { pptdir = file; }
            else if (programtype == "exe" || 
                programtype == "rar" ||
                programtype == "zip" ||
                programtype == "text"|| 
                programtype == "html"||
                programtype == "word") { pptdir = file; args = ""; }
            else { pptdir = file; args = ""; }

            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = args + " \"" + file + "\"";
            // Enter the executable to run, including the complete path
            start.FileName = pptdir;

            int exitCode;

            // Run the external process & wait for it to finish
            try {
                using (Process proc = Process.Start(start))
                {
                    proc.WaitForExit();

                    // Retrieve the app's exit code
                    exitCode = proc.ExitCode;
                }
            } catch(Exception e)
            {
                MessageBox.Show("Error: " + e, "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editPres_Click(object sender, EventArgs e)
        {
            if (openPPTFile == "")
            {
                editPres.Enabled = false;
            }
            else
            {
                try
                {
                    openPPT(openPPTFile, "");
                }
                catch (Exception eventargs)
                {
                    MessageBox.Show("Error: " + eventargs, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
