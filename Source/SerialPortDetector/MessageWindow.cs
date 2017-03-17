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
        private const int DBT_DEVTYP_PORT = 0x00000003;      // serial, parallel

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
            int devType;
            base.WndProc(ref m);
            if (m.Msg == UsbNotification.WmDevicechange)
            {
                switch ((int)m.WParam)
                {
                    case UsbNotification.DbtDeviceremovecomplete:
                        devType = Marshal.ReadInt32(m.LParam, 4);
                        if (devType == DBT_DEVTYP_PORT)
                        {
                            portInfoWindowObj.UpdateList(false);
                        }
                        break;
                    case UsbNotification.DbtDevicearrival:
                        devType = Marshal.ReadInt32(m.LParam, 4);
                        if (devType == DBT_DEVTYP_PORT)
                        {
                            portInfoWindowObj.UpdateList(true);
                        }
                        break;
                }
            }
        }
 
    }
}
