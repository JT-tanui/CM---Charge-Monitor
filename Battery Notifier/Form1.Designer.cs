namespace Battery_Notifier
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            notifyIcon1 = new NotifyIcon(components);
            numericUpDown1 = new NumericUpDown();
            chkEnableNotifications = new CheckBox();
            comboBoxRingtones = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            btnAddRingtone = new Button();
            groupBox1 = new GroupBox();
            lblInstructions = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // notifyIcon1
            // 
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "Battery Notifier";
            notifyIcon1.Visible = true;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(131, 153);
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(150, 27);
            numericUpDown1.TabIndex = 0;
            numericUpDown1.TextAlign = HorizontalAlignment.Center;
            numericUpDown1.Value = new decimal(new int[] { 98, 0, 0, 0 });
            // 
            // chkEnableNotifications
            // 
            chkEnableNotifications.AutoSize = true;
            chkEnableNotifications.Checked = true;
            chkEnableNotifications.CheckState = CheckState.Checked;
            chkEnableNotifications.Location = new Point(126, 186);
            chkEnableNotifications.Name = "chkEnableNotifications";
            chkEnableNotifications.Size = new Size(156, 24);
            chkEnableNotifications.TabIndex = 1;
            chkEnableNotifications.Text = "Enable notification";
            chkEnableNotifications.UseVisualStyleBackColor = true;
            // 
            // comboBoxRingtones
            // 
            comboBoxRingtones.FormattingEnabled = true;
            comboBoxRingtones.Location = new Point(118, 55);
            comboBoxRingtones.Name = "comboBoxRingtones";
            comboBoxRingtones.Size = new Size(170, 28);
            comboBoxRingtones.TabIndex = 2;
            comboBoxRingtones.SelectedIndexChanged += comboBoxRingtones_SelectedIndexChanged_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(35, 23);
            label1.Name = "label1";
            label1.Size = new Size(335, 20);
            label1.TabIndex = 3;
            label1.Text = "Select a ringtone (These are previously imported)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(141, 130);
            label2.Name = "label2";
            label2.Size = new Size(132, 20);
            label2.TabIndex = 4;
            label2.Text = "Battery % to notify";
            // 
            // btnAddRingtone
            // 
            btnAddRingtone.Location = new Point(144, 89);
            btnAddRingtone.Name = "btnAddRingtone";
            btnAddRingtone.Size = new Size(121, 29);
            btnAddRingtone.TabIndex = 5;
            btnAddRingtone.Text = "Add Ringtone";
            btnAddRingtone.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btnAddRingtone);
            groupBox1.Controls.Add(numericUpDown1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(chkEnableNotifications);
            groupBox1.Controls.Add(comboBoxRingtones);
            groupBox1.Location = new Point(211, 183);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(410, 216);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Settings";
            // 
            // lblInstructions
            // 
            lblInstructions.AutoSize = true;
            lblInstructions.Location = new Point(197, 29);
            lblInstructions.Name = "lblInstructions";
            lblInstructions.Size = new Size(480, 140);
            lblInstructions.TabIndex = 7;
            lblInstructions.Text = resources.GetString("lblInstructions.Text");
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblInstructions);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(818, 497);
            MinimumSize = new Size(818, 497);
            Name = "Form1";
            Text = "Battery Notifier";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NotifyIcon notifyIcon1;
        private NumericUpDown numericUpDown1;
        private CheckBox chkEnableNotifications;
        private ComboBox comboBoxRingtones;
        private Label label1;
        private Label label2;
        private Button btnAddRingtone;
        private GroupBox groupBox1;
        private Label lblInstructions;
    }
}