﻿using System;
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

        public Form1()
        {
            InitializeComponent();
            SystemEvents.PowerModeChanged += OnPowerModeChanged;  // Subscribe to power mode changes
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(1000, "Battery Notifier", "Running in the system tray.", ToolTipIcon.Info);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Hide the form at startup
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;

            // Initialize the timer
            batteryCheckTimer = new System.Windows.Forms.Timer();
            batteryCheckTimer.Interval = 20000; // 20 seconds
            batteryCheckTimer.Tick += BatteryCheckTimer_Tick;
            batteryCheckTimer.Start();

            // Set NotifyIcon properties
            notifyIcon1.Text = "Battery Notifier";
            notifyIcon1.Visible = true;

            // Set default numeric value for threshold
            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = 100;
            numericUpDown1.Value = 90;  // Set initial default value

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
                this.Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void NotifyUser()
        {
            PlayCustomSound(@"C:\Users\HP\Documents\Android_dev\Sounds\hip_hop.mp3");

            // Show the message box and wait for the user to press "OK"
            MessageBox.Show("Battery is at " + batteryThreshold + "%! Please unplug your charger.",
                "Battery Notifier", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                    this.Hide();
                    notifyIcon1.Visible = true;
                }
            }
        }
    }
}
