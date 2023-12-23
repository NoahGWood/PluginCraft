using PluginCraft.Core;
using PluginCraftLib.Interfaces;
using PluginCraft.Menus;
using PluginCraft.Panels;

namespace PluginCraft
{
    public static class Program
    {
        static void Main(string[] args)
        {
            App.CreateApp("PluginCraft", 800, 600);
            App.Start();
        }
    }
}
