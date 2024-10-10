using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;  // Add this for MP3 playback
using System.Media;  // Make sure to include this for SoundPlayer
using Microsoft.Win32;  // Add this for SystemEvents

namespace Battery_Notifier
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer batteryCheckTimer;
        int batteryThreshold;
        bool notificationsEnabled = true;  // Global variable to track notification state
        ToolStripMenuItem enableNotificationsMenuItem;  // Reference to the context menu item
        IWavePlayer outputDevice;  // Reference to the audio output device
        Form notificationForm;  // Custom form for notifications
        string notificationSoundPath;  // Path to the notification sound

        public Form1()
        {
            InitializeComponent();
            SystemEvents.PowerModeChanged += OnPowerModeChanged;  // Subscribe to power mode changes

            // Set the notification sound path to the relative path
            notificationSoundPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hip_hop.mp3");
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(1000, "Battery Notifier", $"Running in the system tray.\nBattery Limit: {batteryThreshold}%\nNotification Sound: {System.IO.Path.GetFileName(notificationSoundPath)}", ToolTipIcon.Info);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Hide the form at startup
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;

            // Initialize the timer
            batteryCheckTimer = new System.Windows.Forms.Timer();
            batteryCheckTimer.Interval = 30000; // 30 seconds
            batteryCheckTimer.Tick += BatteryCheckTimer_Tick;
            batteryCheckTimer.Start();

            // Set NotifyIcon properties
            notifyIcon1.Text = "Battery Notifier";
            notifyIcon1.Visible = true;

            // Set default numeric value for threshold
            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = 100;
            numericUpDown1.Value = 94;  // Set initial default value

            // Set initial battery threshold
            batteryThreshold = (int)numericUpDown1.Value;

            // Attach an event to listen to changes in NumericUpDown value
            numericUpDown1.ValueChanged += NumericUpDown1_ValueChanged;

            // Attach the Resize event to hide the form when minimized
            this.Resize += new EventHandler(Form1_Resize);

            // Handle notifications toggle through a checkbox (add a checkbox named chkEnableNotifications in the form)
            chkEnableNotifications.Checked = true;  // Default to enabled
            chkEnableNotifications.CheckedChanged += ChkEnableNotifications_CheckedChanged;

            // Create the context menu for NotifyIcon
            CreateContextMenu();
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            batteryThreshold = (int)numericUpDown1.Value;
        }

        private void BatteryCheckTimer_Tick(object sender, EventArgs e)
        {
            PowerStatus powerStatus = SystemInformation.PowerStatus;

            // Check battery percentage and power status
            Console.WriteLine($"Battery Percentage: {powerStatus.BatteryLifePercent * 100}%");
            Console.WriteLine($"Power Line Status: {powerStatus.PowerLineStatus}");

            if (powerStatus.PowerLineStatus == PowerLineStatus.Online)
            {
                // Charger is plugged in, start monitoring
                if (powerStatus.BatteryLifePercent * 100 >= batteryThreshold && notificationsEnabled)
                {
                    NotifyUser();
                }
            }
            else
            {
                // Charger is unplugged, stop notifications and go dormant
                StopCustomSound();
                CloseNotificationForm();
                this.Hide();
                notifyIcon1.Visible = true;
            }
        }
        private void NotifyUser()
        {
            // Close any existing notification form
            CloseNotificationForm();

            PlayCustomSound(notificationSoundPath);

            // Show the custom notification form
            notificationForm = new Form
            {
                Text = "Battery Notifier",
                Size = new Size(400, 250),
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label messageLabel = new Label
            {
                Text = $"Battery is at {batteryThreshold}%! Please unplug your charger.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            Button okButton = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Dock = DockStyle.Bottom
            };

            okButton.Click += (s, e) => notificationForm.Close();

            notificationForm.Controls.Add(messageLabel);
            notificationForm.Controls.Add(okButton);

            // Create a timer to close the notification form after 20 seconds
            var closeTimer = new System.Windows.Forms.Timer();
            closeTimer.Interval = 20000; // 20 seconds
            closeTimer.Tick += (s, e) =>
            {
                closeTimer.Stop();
                notificationForm.Close();
            };
            closeTimer.Start();

            notificationForm.ShowDialog();

            // Stop the sound when the user presses "OK"
            StopCustomSound();
        }



        private void PlayCustomSound(string filePath)
        {
            try
            {
                var audioFile = new AudioFileReader(filePath);
                outputDevice = new WaveOutEvent();
                outputDevice.Init(audioFile);
                outputDevice.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing sound: " + ex.Message);
            }
        }

        private void StopCustomSound()
        {
            if (outputDevice != null)
            {
                outputDevice.Stop();
                outputDevice.Dispose();
                outputDevice = null;
            }
        }

        private void CloseNotificationForm()
        {
            if (notificationForm != null)
            {
                notificationForm.Close();
                notificationForm = null;
            }
        }

        private void CreateContextMenu()
        {
            // Create context menu for NotifyIcon
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            // "Settings" menu item to show the form
            ToolStripMenuItem settingsMenuItem = new ToolStripMenuItem("Settings");
            settingsMenuItem.Click += SettingsMenuItem_Click;

            // "Enable Notifications" toggle in tray menu
            enableNotificationsMenuItem = new ToolStripMenuItem("Enable Notifications");
            enableNotificationsMenuItem.Checked = notificationsEnabled;
            enableNotificationsMenuItem.CheckOnClick = true;
            enableNotificationsMenuItem.CheckedChanged += EnableNotificationsMenuItem_CheckedChanged;

            // "Exit" menu item to close the app
            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("Exit");
            exitMenuItem.Click += ExitMenuItem_Click;

            // Add items to context menu
            contextMenu.Items.Add(settingsMenuItem);
            contextMenu.Items.Add(enableNotificationsMenuItem);
            contextMenu.Items.Add(exitMenuItem);

            // Assign context menu to NotifyIcon
            notifyIcon1.ContextMenuStrip = contextMenu;
        }

        private void SettingsMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            notifyIcon1.Visible = false;
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ChkEnableNotifications_CheckedChanged(object sender, EventArgs e)
        {
            notificationsEnabled = chkEnableNotifications.Checked;
            enableNotificationsMenuItem.Checked = chkEnableNotifications.Checked;  // Synchronize with context menu
        }

        private void EnableNotificationsMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            notificationsEnabled = enableNotificationsMenuItem.Checked;
            chkEnableNotifications.Checked = enableNotificationsMenuItem.Checked;  // Synchronize with checkbox
        }

        private void OnPowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode == PowerModes.StatusChange)
            {
                PowerStatus powerStatus = SystemInformation.PowerStatus;
                if (powerStatus.PowerLineStatus == PowerLineStatus.Online)
                {
                    // Charger is plugged in, start monitoring
                    batteryCheckTimer.Start();
                }
                else
                {
                    // Charger is unplugged, stop notifications and go dormant
                    batteryCheckTimer.Stop();
                    StopCustomSound();
                    CloseNotificationForm();
                    this.Hide();
                    notifyIcon1.Visible = true;
                }
            }
        }
    }
}
