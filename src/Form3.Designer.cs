namespace ACC_Kiosk
{
    partial class Settings
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.IPLabel = new System.Windows.Forms.Label();
            this.RoomName = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.PCNameLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderSelect = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.confNameText = new System.Windows.Forms.TextBox();
            this.shortcutButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.bgSelect = new System.Windows.Forms.Button();
            this.defaultbgButton = new System.Windows.Forms.Button();
            this.bgImageText = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Room/Hall Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 275);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "This PC IP:";
            // 
            // IPLabel
            // 
            this.IPLabel.AutoSize = true;
            this.IPLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IPLabel.ForeColor = System.Drawing.Color.Red;
            this.IPLabel.Location = new System.Drawing.Point(99, 275);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(60, 13);
            this.IPLabel.TabIndex = 3;
            this.IPLabel.Text = "Unknown";
            // 
            // RoomName
            // 
            this.RoomName.Location = new System.Drawing.Point(16, 30);
            this.RoomName.MaxLength = 80;
            this.RoomName.Name = "RoomName";
            this.RoomName.Size = new System.Drawing.Size(232, 20);
            this.RoomName.TabIndex = 4;
            this.RoomName.TextChanged += new System.EventHandler(this.RoomName_TextChanged);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(92, 322);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 5;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(173, 322);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 302);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "This PC Name:";
            // 
            // PCNameLabel
            // 
            this.PCNameLabel.AutoSize = true;
            this.PCNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PCNameLabel.ForeColor = System.Drawing.Color.Red;
            this.PCNameLabel.Location = new System.Drawing.Point(99, 302);
            this.PCNameLabel.Name = "PCNameLabel";
            this.PCNameLabel.Size = new System.Drawing.Size(60, 13);
            this.PCNameLabel.TabIndex = 8;
            this.PCNameLabel.Text = "Unknown";
            this.PCNameLabel.Click += new System.EventHandler(this.PCNameLabel_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 114);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(151, 20);
            this.textBox1.TabIndex = 9;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Default Directory:";
            // 
            // folderSelect
            // 
            this.folderSelect.Location = new System.Drawing.Point(173, 112);
            this.folderSelect.Name = "folderSelect";
            this.folderSelect.Size = new System.Drawing.Size(72, 23);
            this.folderSelect.TabIndex = 11;
            this.folderSelect.Text = "Browse...";
            this.folderSelect.UseVisualStyleBackColor = true;
            this.folderSelect.Click += new System.EventHandler(this.folderSelect_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(16, 249);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(229, 23);
            this.clearButton.TabIndex = 12;
            this.clearButton.Text = "Clear Settings (Delete Presentation List)";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Conference Name:";
            // 
            // confNameText
            // 
            this.confNameText.Location = new System.Drawing.Point(16, 74);
            this.confNameText.Name = "confNameText";
            this.confNameText.Size = new System.Drawing.Size(229, 20);
            this.confNameText.TabIndex = 14;
            this.confNameText.TextChanged += new System.EventHandler(this.confNameText_TextChanged);
            // 
            // shortcutButton
            // 
            this.shortcutButton.Location = new System.Drawing.Point(15, 220);
            this.shortcutButton.Name = "shortcutButton";
            this.shortcutButton.Size = new System.Drawing.Size(230, 23);
            this.shortcutButton.TabIndex = 15;
            this.shortcutButton.Text = "Create Shortcut on Desktop";
            this.shortcutButton.UseVisualStyleBackColor = true;
            this.shortcutButton.Click += new System.EventHandler(this.shortcutButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(238, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Background Image (1920 x 1080 recommended):";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // bgSelect
            // 
            this.bgSelect.Location = new System.Drawing.Point(173, 162);
            this.bgSelect.Name = "bgSelect";
            this.bgSelect.Size = new System.Drawing.Size(75, 23);
            this.bgSelect.TabIndex = 17;
            this.bgSelect.Text = "Browse...";
            this.bgSelect.UseVisualStyleBackColor = true;
            this.bgSelect.Click += new System.EventHandler(this.bgSelect_Click);
            // 
            // defaultbgButton
            // 
            this.defaultbgButton.Location = new System.Drawing.Point(16, 191);
            this.defaultbgButton.Name = "defaultbgButton";
            this.defaultbgButton.Size = new System.Drawing.Size(232, 23);
            this.defaultbgButton.TabIndex = 18;
            this.defaultbgButton.Text = "Change Background Image to ACC Image";
            this.defaultbgButton.UseVisualStyleBackColor = true;
            this.defaultbgButton.Visible = false;
            this.defaultbgButton.Click += new System.EventHandler(this.defaultbgButton_Click);
            // 
            // bgImageText
            // 
            this.bgImageText.Location = new System.Drawing.Point(16, 164);
            this.bgImageText.Name = "bgImageText";
            this.bgImageText.ReadOnly = true;
            this.bgImageText.Size = new System.Drawing.Size(151, 20);
            this.bgImageText.TabIndex = 19;
            this.bgImageText.TextChanged += new System.EventHandler(this.bgImageText_TextChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(259, 357);
            this.Controls.Add(this.bgImageText);
            this.Controls.Add(this.defaultbgButton);
            this.Controls.Add(this.bgSelect);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.shortcutButton);
            this.Controls.Add(this.confNameText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.folderSelect);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.PCNameLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.RoomName);
            this.Controls.Add(this.IPLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.TextBox RoomName;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label PCNameLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button folderSelect;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox confNameText;
        private System.Windows.Forms.Button shortcutButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button bgSelect;
        private System.Windows.Forms.Button defaultbgButton;
        private System.Windows.Forms.TextBox bgImageText;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}