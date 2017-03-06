using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

// Custom
using System.Xml;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace ACC_Kiosk
{
    public partial class Kiosk : Form
    {
        // fix:
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {

            var fromAddress = new MailAddress("user@gmail.com", "Ben");
            var toAddress = new MailAddress("email address where you want to receive reports", "Your name");
            const string fromPassword = "your password";
            const string subject = "exception report";
            Exception exception = e.Exception;
            string body = exception.Message + "\n" + exception.Data + "\n" + exception.StackTrace + "\n" + exception.Source;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                // You can also use SendAsync method instead of Send so your application begin invoking instead of waiting for send mail to complete. 
                // SendAsync(MailMessage, Object) :- Sends the specified e-mail message to an SMTP server for delivery. 
                // This method does not block the calling thread and allows the caller to pass an object to the method that is invoked when the operation completes. 
                smtp.Send(message);
            }
        }

        private PPTBrowser pptForm = new PPTBrowser();
        private System.Windows.Forms.Timer timer1;
        public string programtype;
        public string openPPTFile;
        public string pptdir;
        public string file;
        public string args;
        
        // URL to update.xml
        public string updateURL = "http://www.bmcstudios.net/projects/acckiosk/update.xml";
        // URL to build executable
        public string buildURL = "http://www.bmcstudios.net/projects/acckiosk/build/acckiosk.exe";

        // Old builds are stored in http://www.bmcstudios.net/projects/acckiosk/build/old/.

        void writeUpdateXML()
        { 
            using (XmlWriter writer = XmlWriter.Create("update.xml"))
            {
                /**************************
                 * Default update.xml:    *
                 * 
                 * <appinfo>
                 *      <version>1.x.x.x</version>
                 *      <url>http://www.url.com</url>
                 *      <about>changelog</about>
                 * </appinfo>
                 * 
                 * ************************/

                writer.WriteStartDocument();
                writer.WriteStartElement("appinfo");

                writer.WriteElementString("version", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
                writer.WriteElementString("url", buildURL);
                writer.WriteElementString("about", "some update here.");
                
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        // Check for updates
        void checkForUpdates()
        {
            DebugLog("Checking for updates...");
            string downloadUrl = "";
            string aboutUpdate = "";
            string xmlUrl = updateURL;
            Version newVersion = null;
            XmlTextReader reader = null;
            Version applicationVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            try
            {
                reader = new XmlTextReader(xmlUrl);
                reader.MoveToContent();
                string elementName = "";
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "appinfo"))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            elementName = reader.Name;
                        }
                        else
                        {
                            if ((reader.NodeType == XmlNodeType.Text) && (reader.HasValue))
                                switch (elementName)
                                {
                                    case "version":
                                        newVersion = new Version(reader.Value);
                                        DebugLog("Current version: " + applicationVersion.Build);
                                        DebugLog("New version: " + newVersion);
                                        break;
                                    case "url":
                                        downloadUrl = reader.Value;
                                        DebugLog("Download URL: " + downloadUrl);
                                        break;
                                    case "about":
                                        aboutUpdate = reader.Value;
                                        DebugLog("About: " + aboutUpdate);
                                        break;
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugLog("Error: " + ex.Message);
                MessageBox.Show(ex.Message);
                Environment.Exit(1);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            if (newVersion > applicationVersion)
            {
                string str = String.Format(
                    "There is a new version available.\n" +
                    "Your version: {0}.\n" +
                    "Newest version: {1}.\n\n" +
                    "Changes in new version: \n{2}. ", applicationVersion, newVersion, aboutUpdate);

                DebugLog("A new version is available, prompting for update...");
                if (DialogResult.No != MessageBox.Show(str + "\nWould you like to download this update?", "Check for updates", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    try
                    {
                        DebugLog("Trying to update to new version...");
                        Process.Start(downloadUrl);
                    }
                    catch (Exception e) {
                        DebugLog("Update failed: " + e);
                        MessageBox.Show("The update was unsuccessful. Try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    return;
                }
            }
            else
            {
                DebugLog("Application version checked and is up-to-date");
            }
        }

        public class PPT
        {
            public string Time;
            public string Day;
            public string Name;
            public string Room;
            public string Path;
            public string File;
            public string Type;

            public PPT() { }
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
                catch(Exception e)
                {
                    DebugLog("Error: there was an issue creating the settings.cfg file. Stack trace: " + e);

                    DialogResult dialogresult = MessageBox.Show("Error creating settings file, is the directory read-only?\nError details: \n" + e.ToString(), "Fatal Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
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

        // Try to load PPT File from openPPT();
        string launchPowerPointFile(string pptdir)
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
            catch (Exception e)
            {
                DebugLog("Error: there was an issue loading the powerpoint file.\nStack trace: " + e);
                MessageBox.Show("Error: " + e, "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            pptdir = dir + "POWERPNT.exe";
            return pptdir;
            // This needs updating to reflect the ppt dir.
            // pptdir = @"C:\Program Files (x86)\Microsoft Office\Office14\POWERPNT.exe";
        }

        // Try to load Adobe file from openPPT();
        string launchAdobeFile()
        {
            // Broken
            //var adobe = Registry.LocalMachine.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows").OpenSubKey("CurrentVersion").OpenSubKey("App Paths").OpenSubKey("AcroRd32.exe");
            //var path = adobe.GetValue("");
            //pptdir = path.ToString();
            //args = "/A \"pagemode = FullScreen\" ";
            return args = "/A \"pagemode = FullScreen\" ";
        }

        // Open PowerPoint function
        void openPPT(string file, string args)
        {
            pptdir = @"";

            switch(programtype)
            {
                // PPT / Adobe / Prezi
                case "ppt":
                    launchPowerPointFile(pptdir);
                    break;
                case "adobe":
                    pptdir = file;
                    launchAdobeFile();
                    break;
                case "prezi":
                    pptdir = file;
                    break;
                
                // other types of files
                case "exe": case "zip": case "text": case "html": case "word":
                    pptdir = file; args = "";
                    break;
            }
            

            // Prepare the process to run,
            // Then enter in command line args,
            // Then enter the executable to run (Complete path)
            ProcessStartInfo start = new ProcessStartInfo();
            start.Arguments = args + " \"" + file + "\"";
            start.FileName = pptdir;

            int exitCode;

            // Run the external process & wait for it to finish
            try
            {
                using (Process proc = Process.Start(start))
                {
                    // Retrieve the app's exit code
                    proc.WaitForExit();
                    exitCode = proc.ExitCode;
                }
            }
            catch (Exception e)
            {
                /* Sometimes this error code will appear 
                 when an error has not actually occurred at all.
                
                 For example:
                 Exception thrown: 'System.NullReferenceException' in ACC Kiosk.exe 
                 */
                DebugLog("Error: Something went wrong trying to launch '" + file + "' with arguments: '" + args + 
                    "'.\nComplete error message: \n" + e);
                MessageBox.Show("Error: " + e, "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            // See if background image has been changed.
            if (!Properties.Settings.Default.bgImage.Contains("Properties.Resources.Adelaide_Convention_Centre"))
            {
                try
                {
                    BackgroundImage = new Bitmap(Properties.Settings.Default.bgImage);
                }
                catch
                {
                    DebugLog("Error: there was an error changing the background image... reverting to default image.\nStack trace: " + e);
                    BackgroundImage = Properties.Resources.Adelaide_Convention_Centre;
                }
            }

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
                            editPres.Enabled = true; StartPresButton.Enabled = true;
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
            Resize += Kiosk_Resize;
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor, true);
        }
        
        private void Kiosk_Resize(object sender, System.EventArgs e)
        {
            this.Update();
        }

        // if CTRL + ALT + END are pressed, exit
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Control)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void AddPres_Click(object sender, EventArgs e)
        {
            DebugLog("Add Presentation clicked.");
            PPTBrowser frm = new PPTBrowser();
            frm.Show();
        }

        private void PresList_DoubleClick(object sender, MouseEventArgs e)
        {
            openPPTFile = PresList.SelectedItems[0].SubItems[4].Text;
            try
            {
                StartPresButton.PerformClick();
            } catch(Exception ex)
            {
                DebugLog("Error: " + ex);
            }
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
            try
            {
                PresList.Items.Add(pptForm.pptFileName.Text);
            } catch(Exception e)
            {
                DebugLog("\nError: " + e);
            }
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            label2.Visible = false;
            Settings frm = new Settings();
            frm.Show();
        }

        private void DebugLog(string s)
        {
            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
            try
            {
                ostrm = new FileStream("./log.txt", FileMode.Append, FileAccess.Write);
                writer = new StreamWriter(ostrm);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open log.txt for writing!");
                Console.WriteLine(e.Message);
                return;
            }
            Console.SetOut(writer);
            Console.WriteLine(DateTime.Now.ToString() + ": " + s);
            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();
            Console.WriteLine("Done");
        }

        // On app start
        private void Kiosk_Load(object sender, EventArgs e)
        {
            DebugLog("\n"+
                "/*********************************************\n" + 
                "*\n*\n" +
                "* Application launched\n*\n" + 
                "* Application version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version + "\n" +
                "* Application run time: " + DateTime.Now.ToString() + "\n" +
                "*\n*\n" +
                "*********************************************/");

            // Write XML file for update.xml
            writeUpdateXML();

            // Check for updates
            checkForUpdates();

            editPres.Enabled = false; StartPresButton.Enabled = false; // Disable until we retrieve list items

            Clock.Text = DateTime.Now.ToString("hh:mm:ss tt");

            // Enable UI
            Properties.Settings.Default.settingsVisible = true;
            Properties.Settings.Default.Save();
            SettingsButton.Visible = true;
            try
            {
                BackgroundImage = new Bitmap(Properties.Settings.Default.bgImage);
            } catch
            {
                BackgroundImage = Properties.Resources.Adelaide_Convention_Centre;
            }

            // Maximize Window
            //FormBorderStyle = FormBorderStyle.None;
            //WindowState = FormWindowState.Maximized;

            // TODO: Make the below code neater. Perhaps implement timer1 & timer2 into a function.
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

        
        /***********************************************************************
         * --- Code below this line has no function
         *     and merely exists due to the fact visual studio creates
         *     the function for the designer.
        ***********************************************************************/

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void Clock_Click(object sender, EventArgs e)
        {

        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
