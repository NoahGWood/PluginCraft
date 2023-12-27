using PluginCraftLib.Interfaces;
using System.IO.Ports;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;
using System.Collections;

namespace GrblControllerPlugin
{
    public class GrblDevice : ISerialDevice
    {
        public SerialPort serialPort { get; private set; }
        private GrblSettings settings;
        public GrblSettings GrblSettings { get { return settings; } }
        public string Name = "";
        public string AllData = "";
        public string Data {get; private set;} = string.Empty;
        public event EventHandler<EventArgs> DataReceived;

        public GrblDevice(string portName, int baudRate)
        {
            serialPort = new SerialPort(portName, baudRate);
            serialPort.DataReceived += SerialPort_DataReceived;
            Name = portName;
            settings = GrblSettings.GenSettings(this);
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Data = serialPort.ReadExisting();
            AllData += Data;
            DataReceived?.Invoke(this, new EventArgs());
        }
        public void Connect()
        {
            try
            {
                // Open the serial port connection
                serialPort.Open();
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log or display an error message)
                Console.WriteLine($"Error connecting to serial device: {ex.Message}");
            }
        }

        public void Disconnect()
        {
            try
            {
                // Close the serial port connection
                serialPort.Close();
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log or display an error message)
                Console.WriteLine($"Error disconnecting from serial device: {ex.Message}");
            }
        }

        public string ReadData()
        {
            try
            {
                return serialPort.ReadLine();
            } catch(Exception ex)
            {
                Console.WriteLine($"Error reading data from serial device: {ex.Message}");
                return "";
            }
        }

        public void WriteData(string data)
        {
            try
            {
                if (!serialPort.IsOpen)
                    Connect();
                serialPort.WriteLine(data);
            } catch(Exception ex)
            {
                Console.WriteLine($"Error writing data to serial device: {ex.Message}");
            }
        }
        public void Jog(string axis, float amt, int speed, bool absolute=false, bool inches=false)
        {
            string cmd = $"$J={axis}{amt} F{speed}";
            if (absolute)
                cmd += " G90";
            else
                cmd += " G91";
            if (inches)
                cmd += " G20";
            else
                cmd += " G21";
            WriteData(cmd);
        }

        public void Home()
        {
            WriteData("$H");
        }
    }
}
