using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace ACC_Kiosk
{

    public partial class PPTBrowser : Form
    {
        private async void Blink()
        {
            if (Properties.Settings.Default.confName == "")
            {

            }
            else
            {
                label5.Text = "Please wait... copying files.";
                while (true)
                {
                    await Task.Delay(500);
                    label5.ForeColor = label5.ForeColor == Color.Red ? Color.Black : Color.Red;
                }
            }
        }
        public TextBox pptFileName
        {
            get
            {
                return pptDirText;
            }
        }
        public PPTBrowser()
        {
            InitializeComponent();
        }

        // set up our variables
        public string pptFile = "";
        public string pptFilePath = "";
        public string path = "";
        public string pptExt = ".ppt";
        public string completeFilePath = ""; // we'll use this later

        private void browseButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}"; //my computer
            openFileDialog1.Title = "Locate your presentation";

            DialogResult result = this.openFileDialog1.ShowDialog();

            if(result == DialogResult.OK)
            {
                this.pptDirText.Text = this.openFileDialog1.FileName;
                // Extract some details from file search
                // into filedir / ext / filename


                pptFile = openFileDialog1.SafeFileName;
                path = openFileDialog1.FileName;
                pptFilePath = path.Replace(pptFile, "");

            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            PPTBrowser.ActiveForm.Close();
        }

        private void pptDirText_TextChanged(object sender, EventArgs e)
        {
            if (PresenterNameText.Text == "" || pptDirText.Text == "")
            {
                OKButton.Enabled = false;
            }
            else
            {
                OKButton.Enabled = true;
            }
        }

        private void copyDirectory(string strSource, string strDestination)
        {
            if (!Directory.Exists(strDestination))
            {
                Directory.CreateDirectory(strDestination);
            }

            DirectoryInfo dirInfo = new DirectoryInfo(strSource);
            FileInfo[] files = dirInfo.GetFiles();
            foreach (FileInfo tempfile in files)
            {
                tempfile.CopyTo(Path.Combine(strDestination, tempfile.Name), true);
            }

            DirectoryInfo[] directories = dirInfo.GetDirectories();
            foreach (DirectoryInfo tempdir in directories)
            {
                // This one shows the standard Windows Copy Dialog with progress bar etc. but shows 'do you wanna overwrite' BS...
                FileSystem.CopyDirectory(Path.Combine(strSource, tempdir.Name), Path.Combine(strDestination, tempdir.Name), UIOption.AllDialogs);

                // This is our copy system but does not show a progress bar.
                //copyDirectory(Path.Combine(strSource, tempdir.Name), Path.Combine(strDestination, tempdir.Name));
            }

        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if(PresenterNameText.Text == "")
            {
                MessageBox.Show("Presenter name cannot be blank.", "Error");
            } if(pptDirText.Text=="")
            {
                MessageBox.Show("Please select a PowerPoint file.", "Error");
            } 
            else
            {
                // Copy the PPT file
                string sourcePath = pptFilePath;

                // Set settings per settings menu
                string confNamewSlash = Properties.Settings.Default.confName + "\\";
                string roomNamewSlash = Properties.Settings.Default.roomName + "\\";

                if(Properties.Settings.Default.confName == "") { confNamewSlash = Properties.Settings.Default.confName; }
                else { confNamewSlash = Properties.Settings.Default.confName + "\\"; }

                if (Properties.Settings.Default.roomName == "") { roomNamewSlash = Properties.Settings.Default.roomName; }
                else { roomNamewSlash = Properties.Settings.Default.roomName + "\\"; }

                string destinationPath = Properties.Settings.Default.defaultDirectory + confNamewSlash + roomNamewSlash;
                string sourceFileName = pptFile;
                
                // Break up saved data for this presentation for a 'nice' filename
                // As follows: TIME - NAME - HALL - DAY
                string destinationFileName = timeTime.Value.ToString("hhmm") + " - " + PresenterNameText.Text + " " + Properties.Settings.Default.roomName.ToString() + " " + dateTime.Value.DayOfWeek.ToString() + " " + pptFile;
                
                // Combine the source & path
                string sourceFile = System.IO.Path.Combine(sourcePath, sourceFileName);
                string destinationFile = System.IO.Path.Combine(destinationPath, destinationFileName);
                completeFilePath = destinationFile;
                
                // If the directory we're saving to does not exist, create it
                if (!System.IO.Directory.Exists(destinationPath))
                {
                    System.IO.Directory.CreateDirectory(destinationPath);
                }
                try {
                    // copy
                    Blink();
                    
                    string dirandfile = pptFilePath + pptFile; // our complete file path with extension
                    string dirtocopy = System.IO.Path.ChangeExtension(dirandfile, null) + "\\"; // cut off the (.ppt) ext and see if the file name is also a folder name


                    // debug stuff
                    //MessageBox.Show(dirtocopy);
                    //MessageBox.Show("Please wait... copying presentation.\nThis may take some time, and the program may freeze.", "", MessageBoxButtons.OK);

                    System.IO.File.Copy(sourceFile, destinationFile, true);

                    string folderPath = Path.GetFileNameWithoutExtension(pptFile);
                    if (System.IO.Directory.Exists(dirtocopy)) // if a folder named the same as the ppt file exists, copy it
                    {
                        copyDirectory(dirtocopy, destinationPath + folderPath); // try to copy the directory if extra media exists for the ppt

                        // Now we will make a second copy of the directory, with our new file name incase PPT tries to point to this folder.
                        destinationFileName = Path.GetFileNameWithoutExtension(destinationFileName);
                        copyDirectory(dirtocopy, destinationPath + destinationFileName.ToString()); // try to copy the directory if extra media exists for the ppt

                    }
                    else if (System.IO.Directory.Exists(sourcePath + "Prezi.app"))
                    {
                        copyDirectory(sourcePath + "Prezi.app", destinationPath);
                        copyDirectory(sourcePath + "content", destinationPath);
                    }
                    else if (System.IO.Directory.Exists(sourcePath + "media")) // if a folder named the same as the ppt file exists, copy it
                    {
                        copyDirectory(sourcePath + "media", destinationPath); // try to copy the directory if extra media exists for the ppt

                        // Now we will make a second copy of the directory, with our new file name incase PPT tries to point to this folder.
                        destinationFileName = Path.GetFileNameWithoutExtension(destinationFileName);
                        copyDirectory(dirtocopy, destinationPath + destinationFileName.ToString()); // try to copy the directory if extra media exists for the ppt
                    }
                    else if (System.IO.Directory.Exists(sourcePath + "content")) // if a folder named the same as the ppt file exists, copy it
                    {
                        copyDirectory(sourcePath + "content", destinationPath); // try to copy the directory if extra media exists for the ppt

                        // Now we will make a second copy of the directory, with our new file name incase PPT tries to point to this folder.
                        destinationFileName = Path.GetFileNameWithoutExtension(destinationFileName);
                        copyDirectory(dirtocopy, destinationPath + destinationFileName.ToString()); // try to copy the directory if extra media exists for the ppt
                    }
                } catch (Exception errormsg)
                {
                    // :(
                    MessageBox.Show("Error copying:\n" + sourceFile.ToString() + "\nto\n" + destinationFile.ToString() + "\n\n" + errormsg, "");
                }

                // Create a string array
                string prestime = "\n" + timeTime.Text.ToString();
                string[] lines = {
                /*
                Output is as follows:
                    Presentation Time: prestime,
                    Presentation Date: dateTime,
                    Presenter Name:    PresenterNameText,
                    Presentation Room: roomName,
                    PPT File:          pptDirText,
                    File Path:         completeFilePath
                */

                    /*"Presentation Time: " + */
                    prestime,

                    /*"Presentation Date: " + */
                    dateTime.Value.DayOfWeek.ToString(),

                    /* "Presenter Name: " + */
                    PresenterNameText.Text,

                    /*"Presentation Room: " + */
                    Properties.Settings.Default.roomName,

                    /*"PPT File: " + */
                    pptDirText.Text,

                    /*"File path: " + */
                    completeFilePath.ToString()
                };

                try
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"settings.cfg", true))
                    {
                        foreach (string line in lines)
                        {
                            file.WriteLine(line);
                        }  file.Close(); // close when we're done
                    }
                    label5.Visible = false; // make the copying notice invisible
                    PPTBrowser.ActiveForm.Close(); // close this as we are done

                }
                catch (Exception eventarg)
                {
                    MessageBox.Show("Unable to write to 'settings.cfg'\nIs the file open?" + eventarg, "Write error", MessageBoxButtons.OK);
                    PPTBrowser.ActiveForm.Close(); // close this as we are done
                }

            }
        }

        private void todayDate_Click(object sender, EventArgs e)
        {
        }

        private void dateList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                OKButton.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void PPTBrowser_Load(object sender, EventArgs e)
        {
            // For 24-hr time
            timeTime.CustomFormat = "HH:mm tt";
            if (PresenterNameText.Text == "" || pptDirText.Text == "")
            {
                OKButton.Enabled = false;
            }
            else
            {
                OKButton.Enabled = true;
            }
        }

        private void PresenterNameText_TextChanged(object sender, EventArgs e)
        {
            if (PresenterNameText.Text == "" || pptDirText.Text == "")
            {
                OKButton.Enabled = false;
            }
            else
            {
                OKButton.Enabled = true;
            }
        }

        private void dateTime_ValueChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
