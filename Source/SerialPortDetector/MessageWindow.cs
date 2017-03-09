using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialPortDetector
{
    class MessageWindow : Form
    {
        private PortInfoForm portInfoWindowObj;

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        public MessageWindow()
        {
            var accessHandle = this.Handle;
            
            // register usb event
            UsbNotification.RegisterUsbDeviceNotification(this.Handle);
        }

        public void SetPortInfoObj( PortInfoForm obj )
        {
            // save port info window obj for usb event update
            portInfoWindowObj = obj;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            ChangeToMessageOnlyWindow();
        }

        private void ChangeToMessageOnlyWindow()
        {
            IntPtr HWND_MESSAGE = new IntPtr(-3);
            SetParent(this.Handle, HWND_MESSAGE);
        }

        protected override void WndProc(ref Message m)
        {            
            base.WndProc(ref m);
            if (m.Msg == UsbNotification.WmDevicechange)
            {
                switch ((int)m.WParam)
                {
                    case UsbNotification.DbtDeviceremovecomplete:
                        portInfoWindowObj.UpdateList(false);
                        break;
                    case UsbNotification.DbtDevicearrival:
                        portInfoWindowObj.UpdateList(true);
                        break;
                }
            }
        }
 
    }
}
