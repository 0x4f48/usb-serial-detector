using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialPortDetector
{
    public class TaskTrayApplicationContext : ApplicationContext
    {
        NotifyIcon notifyIcon = new NotifyIcon();
        PortInfoForm portInfoWindow = new PortInfoForm();
        MessageWindow msgWindow = new MessageWindow();

        public TaskTrayApplicationContext()
        {
            MenuItem configMenuItem = new MenuItem("Show Info", new EventHandler(ShowInfo));
            MenuItem exitMenuItem = new MenuItem("Exit", new EventHandler(Exit));

            notifyIcon.Icon = SerialPortDetector.Properties.Resources.AppIcon;
            notifyIcon.DoubleClick += new EventHandler(ShowMessage);
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] { configMenuItem, exitMenuItem });
            notifyIcon.Visible = true;

            msgWindow.SetPortInfoObj(portInfoWindow);
        }
        void ShowMessage(object sender, EventArgs e)
        {
            // Only show the message if the settings say we can.
            //if (TaskTrayApplication.Properties.Settings.Default.ShowMessage)
            //    MessageBox.Show("Hello World");
        }
        void ShowInfo(object sender, EventArgs e)
        {
            // If we are already showing the window meerly focus it.
            if (portInfoWindow.Visible)
                portInfoWindow.Focus();
            else
                portInfoWindow.ShowDialog();
        }
        void Exit(object sender, EventArgs e)
        {
            // We must manually tidy up and remove the icon before we exit.
            // Otherwise it will be left behind until the user mouses over.
            notifyIcon.Visible = false;
            Application.Exit();
        }
    }
}
