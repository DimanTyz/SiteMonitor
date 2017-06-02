using MonitorService;
using System;
using System.Configuration.Install;
using System.ServiceProcess;
using System.Windows.Forms;

namespace Installer
{
    public partial class Form1 : Form
    {
        ServiceController contr;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                ManagedInstallerClass.InstallHelper(new string[] { Application.StartupPath + @"\MonitorService.exe" });
                contr = new ServiceController();
                contr.ServiceName = "[Site Monitor]";
                contr.Start();
                button2.Enabled = true;
                button1.Enabled = false;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (contr!=null && contr.Status == ServiceControllerStatus.Running)
                {
                    contr = new ServiceController();
                    contr.ServiceName = "[Site Monitor]";
                    contr.Stop();
                    button1.Enabled = true;
                    button2.Enabled = false;
                }
                ManagedInstallerClass.InstallHelper(new string[] { @"/u", Application.StartupPath + @"\MonitorService.exe" });
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (StatusController.CheckStatus(contr))
                MessageBox.Show("Service is working");
            else
                MessageBox.Show("Service is not running");
        }
    }
}
