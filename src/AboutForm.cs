using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACC_Kiosk
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = "ACC Kiosk is a program created by Ben Weidenhofer in 2015.\r\n" +
                "It was programmed in C# and is quite an extensive project!\r\n" +
                "The kiosk is an application designed for conferences as a central point to upload presentations.\r\n" +
                "Presentations can be added, edited, and launched from the app in a simple-to-use manner.\r\n";
            label1.Text = "Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
