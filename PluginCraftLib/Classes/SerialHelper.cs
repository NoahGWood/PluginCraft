using System.IO.Ports;

namespace PluginCraftLib.Classes
{
    public static class SerialHelper
    {
        private static string[]? m_AvailablePorts;
        public static string[] AvailablePorts
        {
            get
            {
                m_AvailablePorts = SerialPort.GetPortNames();
                return m_AvailablePorts;
            }
        }

        public static Task<Stream> OpenStreamAsync(SerialPort port)
        {
            port.Open();
            return Task.FromResult(port.BaseStream);
        }
        public static Task CloseStreamAsync(SerialPort port, Stream stream)
        {
            port.Close();
            return Task.FromResult(true);
        }
        public static void Write(SerialPort port, byte[] buffer, int offset, int count)
        {
            port.Write(buffer, offset, count);
        }
    }
}
