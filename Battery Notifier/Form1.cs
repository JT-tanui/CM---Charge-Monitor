using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Windows.Forms;
using NAudio.Wave;  // Add this for MP3 playback
using System.Media;  // Make sure to include this for SoundPlayer

namespace Battery_Notifier
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer batteryCheckTimer;
        int batteryThreshold;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Hide the form at startup
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;

            // Initialize the timer
            batteryCheckTimer = new System.Windows.Forms.Timer();
            batteryCheckTimer.Interval = 60000; // 1 minute
            batteryCheckTimer.Tick += BatteryCheckTimer_Tick;
            batteryCheckTimer.Start();

            // Set NotifyIcon properties
            notifyIcon1.Text = "Battery Notifier";
            notifyIcon1.Visible = true;

            // Set default numeric value for threshold
            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = 100;
            numericUpDown1.Value = 98;  // Set initial default value

            // Set initial battery threshold
            batteryThreshold = (int)numericUpDown1.Value;

            // Attach an event to listen to changes in NumericUpDown value
            numericUpDown1.ValueChanged += NumericUpDown1_ValueChanged;

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

            // Debug: Log the current battery percentage and power status
            Console.WriteLine($"Battery Percentage: {powerStatus.BatteryLifePercent * 100}%");
            Console.WriteLine($"Power Line Status: {powerStatus.PowerLineStatus}");

            // Check if the battery percentage exceeds the threshold and is plugged in
            if (powerStatus.BatteryLifePercent * 100 >= batteryThreshold && powerStatus.PowerLineStatus == PowerLineStatus.Online)
            {
                NotifyUser();  // Call the notification method
            }
        }


        private void NotifyUser()
        {
            // Show a notification
            MessageBox.Show("Battery is at " + batteryThreshold + "%! Please unplug your charger.", "Battery Notifier", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Play a custom notification sound (WAV)
            PlayCustomSound(@"C:\Users\HP\Documents\Android_dev\Sounds\hip_hop.mp3");

        }

        private void PlayCustomSound(string filePath)
        {
            try
            {
                using (SoundPlayer player = new SoundPlayer(filePath))
                {
                    player.PlaySync(); // Plays the sound synchronously
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing sound: " + ex.Message);
            }
        }


        private void CreateContextMenu()
        {
            // Create context menu for NotifyIcon
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            // Create "Settings" menu item to show the form
            ToolStripMenuItem settingsMenuItem = new ToolStripMenuItem("Settings");
            settingsMenuItem.Click += SettingsMenuItem_Click;

            // Create "Exit" menu item to close the app
            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("Exit");
            exitMenuItem.Click += ExitMenuItem_Click;

            // Add the items to the context menu
            contextMenu.Items.Add(settingsMenuItem);
            contextMenu.Items.Add(exitMenuItem);

            // Assign the context menu to the NotifyIcon
            notifyIcon1.ContextMenuStrip = contextMenu;
        }

        private void SettingsMenuItem_Click(object sender, EventArgs e)
        {
            // Show the form again to allow threshold adjustment
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            // Close the application
            Application.Exit();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
