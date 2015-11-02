namespace ACC_Kiosk
{
    partial class PPTBrowser
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
            this.label1 = new System.Windows.Forms.Label();
            this.PresenterNameText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pptDirText = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTime = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.timeTime = new System.Windows.Forms.DateTimePicker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Presenter Name:";
            // 
            // PresenterNameText
            // 
            this.PresenterNameText.Location = new System.Drawing.Point(16, 30);
            this.PresenterNameText.Name = "PresenterNameText";
            this.PresenterNameText.Size = new System.Drawing.Size(264, 20);
            this.PresenterNameText.TabIndex = 1;
            this.PresenterNameText.TextChanged += new System.EventHandler(this.PresenterNameText_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Select your PowerPoint file...";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // pptDirText
            // 
            this.pptDirText.Location = new System.Drawing.Point(16, 91);
            this.pptDirText.Name = "pptDirText";
            this.pptDirText.ReadOnly = true;
            this.pptDirText.Size = new System.Drawing.Size(264, 20);
            this.pptDirText.TabIndex = 3;
            this.pptDirText.TextChanged += new System.EventHandler(this.pptDirText_TextChanged);
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(286, 88);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 4;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(204, 234);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 5;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(286, 234);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 6;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Day of presentation";
            // 
            // dateTime
            // 
            this.dateTime.Location = new System.Drawing.Point(16, 147);
            this.dateTime.Name = "dateTime";
            this.dateTime.Size = new System.Drawing.Size(197, 20);
            this.dateTime.TabIndex = 11;
            this.dateTime.ValueChanged += new System.EventHandler(this.dateTime_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(235, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Presentation Time:";
            // 
            // timeTime
            // 
            this.timeTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timeTime.Location = new System.Drawing.Point(238, 146);
            this.timeTime.Name = "timeTime";
            this.timeTime.ShowUpDown = true;
            this.timeTime.Size = new System.Drawing.Size(92, 20);
            this.timeTime.TabIndex = 13;
            this.timeTime.Value = new System.DateTime(2015, 7, 8, 12, 0, 0, 0);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(37, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(280, 25);
            this.label4.TabIndex = 14;
            this.label4.Text = "Please wait - copying files...";
            this.label4.Visible = false;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // PPTBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 269);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.timeTime);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dateTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.pptDirText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PresenterNameText);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PPTBrowser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add your presentation";
            this.Load += new System.EventHandler(this.PPTBrowser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PresenterNameText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox pptDirText;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button OKButton;
        private new System.Windows.Forms.Button CancelButton; // 'new' added to avoid error "Use the new keyword if hiding was intended(CS0108)"
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker timeTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label4;
    }
}