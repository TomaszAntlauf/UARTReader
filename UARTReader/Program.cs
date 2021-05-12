using System;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading;

namespace UARTReader
{
    class Program
    {
        static bool _continue;
        static SerialPort _serialPort;

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        const uint KEYEVENTF_KEYUP = 0x0002;
        static void Main(string[] args)
        {
            Thread readThread = new Thread(Read);

            _serialPort = new SerialPort();

            bool exit = true;
            while (exit)
            {
                try
                {

                    _serialPort.PortName = SetPortName(_serialPort.PortName);
                    _serialPort.BaudRate = 9600;
                    _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), "None", true);
                    _serialPort.DataBits = 8;
                    _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One", true);
                    _serialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), "None", true);

                    _serialPort.ReadTimeout = 100000;
                    _serialPort.WriteTimeout = 50;

                    _serialPort.Open();
                    exit = false;
                }
                catch (Exception ex) { }
            }
            
            _continue = true;
            readThread.Start();

        }

        public static void Read()
        {
            while (_continue)
            {
                try
                {
                    string message = _serialPort.ReadLine();
                    if (!String.IsNullOrEmpty(message))
                        Console.WriteLine(message);

                    //up
                    if (message.Contains("d1"))
                    {
                        keybd_event((byte)0x57, 0, 0, 0);
                    }
                    else if (message.Contains("u1"))
                    {
                        keybd_event((byte)0x57, 0, KEYEVENTF_KEYUP | 0, 0);
                    }

                    //down
                    if (message.Contains("d4"))
                    {
                        keybd_event((byte)0x53, 0, 0, 0);
                    }
                    else if (message.Contains("u4"))
                    {
                        keybd_event((byte)0x53, 0, KEYEVENTF_KEYUP | 0, 0);
                    }

                    //left
                    if (message.Contains("d2"))
                    {
                        keybd_event((byte)0x41, 0, 0, 0);
                    }
                    else if (message.Contains("u2"))
                    {
                        keybd_event((byte)0x41, 0, KEYEVENTF_KEYUP | 0, 0);
                    }

                    //right
                    if (message.Contains("d3"))
                    {
                        keybd_event((byte)0x44, 0, 0, 0);
                    }
                    else if (message.Contains("u3"))
                    {
                        keybd_event((byte)0x44, 0, KEYEVENTF_KEYUP | 0, 0);
                    }

                    //action 1
                    else if (message.Contains("d5"))
                    {
                        keybd_event((byte)0x49, 0, 0, 0);
                    }
                    else if (message.Contains("u5"))
                    {
                        keybd_event((byte)0x49, 0, KEYEVENTF_KEYUP | 0, 0);
                    }

                    //action 2
                    else if (message.Contains("d6"))
                    {
                        keybd_event((byte)0x4F, 0, 0, 0);
                    }
                    else if (message.Contains("u6"))
                    {
                        keybd_event((byte)0x4F, 0, KEYEVENTF_KEYUP | 0, 0);
                    }

                    //action 3
                    else if (message.Contains("d7"))
                    {
                        keybd_event((byte)0x50, 0, 0, 0);
                    }
                    else if (message.Contains("u7"))
                    {
                        keybd_event((byte)0x50, 0, KEYEVENTF_KEYUP | 0, 0);
                    }

                    //action 4
                    else if (message.Contains("d8"))
                    {
                        keybd_event((byte)0x4A, 0, 0, 0);
                    }
                    else if (message.Contains("u8"))
                    {
                        keybd_event((byte)0x4A, 0, KEYEVENTF_KEYUP | 0, 0);
                    }

                    //action 5
                    else if (message.Contains("d9"))
                    {
                        keybd_event((byte)0x4B, 0, 0, 0);
                    }
                    else if (message.Contains("u9"))
                    {
                        keybd_event((byte)0x4B, 0, KEYEVENTF_KEYUP | 0, 0);
                    }

                    //action 6
                    else if (message.Contains("d10"))
                    {
                        keybd_event((byte)0x4C, 0, 0, 0);
                    }
                    else if (message.Contains("u10"))
                    {
                        keybd_event((byte)0x4C, 0, KEYEVENTF_KEYUP | 0, 0);
                    }


                    Console.WriteLine(message);
                }
                catch (TimeoutException) { }
            }
        }

        public static string SetPortName(string defaultPortName)
        {
            string portName;

            Console.WriteLine("Available Ports:");
            foreach (string s in SerialPort.GetPortNames())
            {
                Console.WriteLine("   {0}", s);
            }

            Console.Write("Enter COM port value (Default: {0}): ", defaultPortName);
            portName = Console.ReadLine();

            if (portName == "" || !(portName.ToLower()).StartsWith("com"))
            {
                portName = defaultPortName;
            }
            return portName;
        }
    }
}
