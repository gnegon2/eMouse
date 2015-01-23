using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Permissions;

namespace eMouse
{
    public partial class eMouse : Form
    {

        public static bool Reading = false;
        private Thread oThread;
        private Reader readerObject;
        private SerialPort serial_port;

        string portName; 
        int baudRate; 
        Parity parity = Parity.None;
        int dataBits = 8;
        StopBits stopBits = StopBits.One;

        public eMouse()
        {
            InitializeComponent();           
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, ControlThread = true)]
        private void CloseReading()
        {
            Reading = false;
        }

        private void eMouse_OnLoad(object sender, EventArgs e)
        {
            Baudrate.Text = "9600";
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
                COM.Items.Add(port);
            COM.SelectedIndex = 0;
        }

        private void Start_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (oThread == null)
                {
                    portName = COM.Text;
                    baudRate = Convert.ToInt32(Baudrate.Text);
                    serial_port = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
                    serial_port.ReadTimeout = 500;
                    Start.Text = "STOP";
                    Reading = true;
                    readerObject = new Reader(debug, serial_port);
                    oThread = new Thread(readerObject.Reading);
                    oThread.Start();
                }
                else
                {
                    CloseReading();
                    oThread = null;
                    Start.Text = "START";
                    serial_port.Close();
                    debug.Text = "";
                }
            }
            catch (Exception ex)
            {
                debug.Text = ex.Message;
            }
        }

        private void eMouse_OnClosing(object sender, FormClosingEventArgs e)
        {
            if (oThread != null)
            {
                CloseReading();
                oThread = null;
            }
        }
    }
}
