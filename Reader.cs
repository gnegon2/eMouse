using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eMouse
{
    public class Reader
    {
        private SerialPort serial_port;
        private ASCIIEncoding encoder;
        private Decoder decoder;
        private TextBox debug;

        private int dpi = 0;
        private bool LeftClicking, LeftClicked, RightClicking, RightClicked;
        private String text;

        private String TimeoutError = "Lost connection.";

        public Reader(TextBox debug, SerialPort serial_port)
        {
            this.debug = debug;
            this.serial_port = serial_port;
        }

        public void Reading()
        {
            encoder = new ASCIIEncoding();
            decoder = encoder.GetDecoder();
            try
            {
                serial_port.Open();
            }
            catch (Exception ex)
            {
                SetControlPropertyThreadSafe(debug, "Text", ex.Message);
                eMouse.Reading = false;
            }
            LeftClicked = RightClicked = false;
            while (eMouse.Reading)
            {
                int eX, tempX = 0, eY, tempY = 0, switches = 0;
                text = ReadMessage(serial_port);
                if( text.Length > 0)
                    SetControlPropertyThreadSafe(debug, "Text", text);
                try
                {
                    tempX = Convert.ToInt16(text.Substring(0, 3));
                    tempY = Convert.ToInt16(text.Substring(3, 3));
                    switches = Convert.ToInt16(text.Substring(6, 1));
                }
                catch { continue; }
                try
                {
                    LeftClicking = Convert.ToBoolean(switches & 2);
                    RightClicking = Convert.ToBoolean(switches & 1);
                    if (LeftClicked && !LeftClicking)
                    {
                        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
                        LeftClicked = false;
                    }
                    if (RightClicked && !RightClicking)
                    {
                        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightUp);
                        RightClicked = false;
                    }
                    if (LeftClicking && !LeftClicked)
                    {
                        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                        LeftClicked = true;
                    }
                    if (RightClicking && !RightClicked)
                    {
                        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightDown);
                        RightClicked = true;
                    }
                    eX = tempX < 128 ? tempX : tempX - 255;
                    eY = tempY < 128 ? tempY : tempY - 255;
                    if (eX > dpi || eX < -dpi || eY > dpi || eY < -dpi)
                        Cursor.Position = new Point(Cursor.Position.X - eX / 3, Cursor.Position.Y + eY / 3);
                }
                catch (Exception ex) 
                {
                    SetControlPropertyThreadSafe(debug, "Text", ex.Message);
                    serial_port.Close();
                    eMouse.Reading = false;
                }
            }
        }

        public String ReadMessage(SerialPort sp)
        {
            byte[] read_byte = new byte[7];
            char[] read_chars = new char[7];
            String message = "";
            try
            {
                do
                {
                    sp.Read(read_byte, 0, 1);
                } while (read_byte[0] != 58);
                sp.DiscardInBuffer();
                for( int i = 0; i < 500; i++)
                {
                    Thread.Sleep(1);
                    if (sp.BytesToRead > 6)
                        break;
                    if (i == 499)
                    {
                        SetControlPropertyThreadSafe(debug, "Text", TimeoutError);
                        sp.Close();
                        eMouse.Reading = false;
                        return message;
                    }
                }
                sp.Read(read_byte, 0, 7);
                decoder.GetChars(read_byte, 0, 7, read_chars, 0, true);
                foreach (char c in read_chars)
                    message += c;
            }
            catch (Exception ex) 
            {
                if( ex is System.TimeoutException )
                    SetControlPropertyThreadSafe(debug, "Text", TimeoutError);
                try
                {
                    sp.Close();
                }
                catch (Exception) { }
                eMouse.Reading = false;
            }               
            return message;
        }

        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        public static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            try
            {
                if (control.InvokeRequired)
                {
                    control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe), new object[] { control, propertyName, propertyValue });
                }
                else
                {
                    control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new object[] { propertyValue });
                }
            }
            catch (Exception) { }
        }
    }
}
