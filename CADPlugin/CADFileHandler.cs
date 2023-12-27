using PluginCraftLib.Interfaces;

namespace CADPlugin
{
    internal class CADFileHandler : IFileHandler<string>
    {
        public string FileType => "CAD";

        public string Extensions => "stl, ply, obj, fbx, vtk, step, iges, brep";

        public string ReadFile(string filePath)
        {
            return $"READING FILE {filePath}";
        }

        public void SaveFile(string filePath)
        {
            Console.WriteLine($"Saving to {filePath}");
        }

        public void WriteFile(string filePath, string data)
        {
            Console.WriteLine($"Writing {data} to {filePath}");
        }
    }
}
