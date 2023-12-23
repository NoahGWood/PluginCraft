namespace PluginCraftLib.Interfaces
{
    public interface ISerialDevice
    {
        // Connect to the serial device
        void Connect();
        // Disconnect from the serial device
        void Disconnect();
        // Write data to the serial device
        void WriteData(string data);
        // Read data from the serial device
        string ReadData();
        // Event raised when data is received from the serial device
        event EventHandler<EventArgs> DataReceived;
    }
}
