namespace PluginCraftLib.Interfaces
{
    public interface IFileHandler<T>
    {
        T ReadFile(string filePath);
        void WriteFile(string filePath, T data);
    }
}
