using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Management;
using System.Diagnostics;
using System.Threading;

namespace SerialPortDetector
{
    public partial class PortInfoForm : Form
    {
        private BackgroundWorker bgwUpdater;
        private bool bShowWindow = true;

        public PortInfoForm()
        {
            InitializeComponent();
            this.bgwUpdater = new System.ComponentModel.BackgroundWorker();
            this.bgwUpdater.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwUpdater_RunWorkerCompleted);
        }
        private void Populate_Port_List()
        {
            ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_PnPEntity");

            foreach (ManagementObject queryObj in searcher.Get())
            {
                if (queryObj["Caption"].ToString().Contains("(COM"))
                {
                    lbPortList.Items.Add( String.Format("{0}", queryObj["Caption"]) );
                }
            }
        }
        
        private void UpdatePortInfo()
        {
            lbPortList.Items.Clear();
            Populate_Port_List();
        }

        private void FormLoaded(object sender, EventArgs e)
        {
            UpdatePortInfo();
        }

        private void bgwUpdater_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UpdatePortInfo();

            if (this.bShowWindow)
            {
                // show up window
                if (this.Visible == true)
                    this.Focus();
                else
                    this.ShowDialog();
            }
        }

        public void UpdateList(bool bShowWindow)
        {
            this.bShowWindow = bShowWindow;
            if (!this.bgwUpdater.IsBusy)
                this.bgwUpdater.RunWorkerAsync();
        }

    }
}
