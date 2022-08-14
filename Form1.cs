using System.Management;
using System;
using System.Management.Automation;
using Microsoft.Win32;
using System.Resources;
using io_lockdown.Properties;

namespace io_lockdown
{
    public partial class Form1 : Form
    {

        private NotifyIcon trayIcon;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
            // Initialize Tray Icon
            trayIcon = new NotifyIcon();
            trayIcon.Visible = true;
        }

        private void Form1_Closed(object sender, EventArgs e)
        {
            Microsoft.Win32.SystemEvents.SessionSwitch -= new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            trayIcon.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {


            SelectQuery wmiQuery = new SelectQuery("SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionId != NULL");
            ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(wmiQuery);
            foreach (ManagementObject item in searchProcedure.Get())
            {
                item.InvokeMethod("Disable", null);
                MessageBox.Show((string)item["NetConnectionId"] + " disabled");

            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            SelectQuery wmiQuery = new SelectQuery("SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionId != NULL");
            ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(wmiQuery);
            foreach (ManagementObject item in searchProcedure.Get())
            {                
                item.InvokeMethod("Enable", null);
                MessageBox.Show((string)item["NetConnectionId"] + " enabled");

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
        }

        void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            string time = DateTime.Now.ToString("h:mm:ss tt");
            switch (e.Reason)
            {                
                case SessionSwitchReason.SessionLogon:                    
                case SessionSwitchReason.SessionUnlock:
                    // Desativa interfaces de rede, portas USB (exceto a do teclado), firewire e Bluetooth
                    MessageBox.Show("Unlock " + time);
                    break;
                case SessionSwitchReason.SessionLock:                    
                case SessionSwitchReason.SessionLogoff:
                    MessageBox.Show("Lock " + time);
                    break;
            }
        }

    }
}