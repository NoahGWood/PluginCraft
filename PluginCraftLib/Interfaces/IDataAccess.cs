namespace PluginCraftLib.Interfaces
{
    public interface IDataAccess<T>
    {
        T ReadData();
        void WriteData(T data);
    }
}
