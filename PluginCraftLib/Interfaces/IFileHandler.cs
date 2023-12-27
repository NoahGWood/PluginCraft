namespace PluginCraftLib.Interfaces
{
    public interface IFileHandler<T>
    {
        string FileType { get; } // File type, e.g. image, document, etc.
        string Extensions { get; } // Supported extensions
        T ReadFile(string filePath);
        void WriteFile(string filePath, T data);
        void SaveFile(string filePath);
    }
}
