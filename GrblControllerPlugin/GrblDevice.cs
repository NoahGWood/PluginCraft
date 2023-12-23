using PluginCraftLib.Interfaces;
using System.IO.Ports;
using System.Diagnostics;

namespace GrblControllerPlugin
{
    public class GrblDevice : ISerialDevice
    {
        private SerialPort serialPort;
        public string Data {get; private set;} = string.Empty;
        public event EventHandler<EventArgs> DataReceived;

        public GrblDevice(string portName, int baudRate)
        {
            serialPort = new SerialPort(portName, baudRate);
            serialPort.DataReceived += SerialPort_DataReceived;
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Data = serialPort.ReadLine();
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
                return null;
            }
        }

        public void WriteData(string data)
        {
            try
            {
                serialPort.Write(data);
            } catch(Exception ex)
            {
                Console.WriteLine($"Error writing data to serial device: {ex.Message}");
            }
        }
        private void SerialPort_DataReceived(object sender, DataReceivedEventArgs e)
        {
        }
    }
}
