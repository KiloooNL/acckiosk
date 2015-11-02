namespace ACC_Kiosk
{
    partial class Kiosk
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Kiosk));
            this.AddPres = new System.Windows.Forms.Button();
            this.PresList = new System.Windows.Forms.ListView();
            this.StartPresButton = new System.Windows.Forms.Button();
            this.editPres = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.Clock = new System.Windows.Forms.Label();
            this.SettingsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AddPres
            // 
            this.AddPres.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddPres.Location = new System.Drawing.Point(527, 399);
            this.AddPres.Name = "AddPres";
            this.AddPres.Size = new System.Drawing.Size(139, 38);
            this.AddPres.TabIndex = 0;
            this.AddPres.Text = "Add Presentation";
            this.AddPres.UseVisualStyleBackColor = true;
            this.AddPres.Click += new System.EventHandler(this.AddPres_Click);
            // 
            // PresList
            // 
            this.PresList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.PresList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PresList.GridLines = true;
            this.PresList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.PresList.LabelWrap = false;
            this.PresList.Location = new System.Drawing.Point(446, 68);
            this.PresList.Margin = new System.Windows.Forms.Padding(0);
            this.PresList.MultiSelect = false;
            this.PresList.Name = "PresList";
            this.PresList.Size = new System.Drawing.Size(474, 329);
            this.PresList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.PresList.TabIndex = 1;
            this.PresList.UseCompatibleStateImageBehavior = false;
            this.PresList.View = System.Windows.Forms.View.List;
            this.PresList.SelectedIndexChanged += new System.EventHandler(this.PresList_SelectedIndexChanged);
            // 
            // StartPresButton
            // 
            this.StartPresButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StartPresButton.Location = new System.Drawing.Point(789, 400);
            this.StartPresButton.Name = "StartPresButton";
            this.StartPresButton.Size = new System.Drawing.Size(128, 37);
            this.StartPresButton.TabIndex = 2;
            this.StartPresButton.Text = "Start Presentation";
            this.StartPresButton.UseVisualStyleBackColor = true;
            this.StartPresButton.Click += new System.EventHandler(this.StartPresButton_Click);
            // 
            // editPres
            // 
            this.editPres.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.editPres.Location = new System.Drawing.Point(672, 400);
            this.editPres.Name = "editPres";
            this.editPres.Size = new System.Drawing.Size(111, 37);
            this.editPres.TabIndex = 5;
            this.editPres.Text = "Edit Presentation";
            this.editPres.UseVisualStyleBackColor = true;
            this.editPres.Click += new System.EventHandler(this.editPres_Click);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(524, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(312, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Check conference and hall/room name in settings --->";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // Clock
            // 
            this.Clock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Clock.AutoSize = true;
            this.Clock.BackColor = System.Drawing.Color.Transparent;
            this.Clock.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clock.ForeColor = System.Drawing.Color.White;
            this.Clock.Location = new System.Drawing.Point(760, 32);
            this.Clock.Name = "Clock";
            this.Clock.Size = new System.Drawing.Size(76, 29);
            this.Clock.TabIndex = 6;
            this.Clock.Text = "12:00";
            // 
            // SettingsButton
            // 
            this.SettingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SettingsButton.Location = new System.Drawing.Point(842, 9);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(75, 23);
            this.SettingsButton.TabIndex = 4;
            this.SettingsButton.Text = "Settings";
            this.SettingsButton.UseVisualStyleBackColor = true;
            this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // Kiosk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = global::ACC_Kiosk.Properties.Resources.Adelaide_Convention_Centre;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(929, 449);
            this.Controls.Add(this.Clock);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.editPres);
            this.Controls.Add(this.StartPresButton);
            this.Controls.Add(this.PresList);
            this.Controls.Add(this.AddPres);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Kiosk";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ACC Kiosk";
            this.Load += new System.EventHandler(this.Kiosk_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddPres;
        private System.Windows.Forms.ListView PresList;
        private System.Windows.Forms.Button StartPresButton;
        private System.Windows.Forms.Button editPres;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Clock;
        private System.Windows.Forms.Button SettingsButton;
    }
}

