using PluginCraftLib.Interfaces;
using PluginCraftLib.Classes;
using System.Reflection;
using PluginCraft.Core;


namespace PluginCraft
{
    public static class PluginLoader
    {
        private static List<Plugin> plugins = new List<Plugin>();
        public static List<Plugin> Plugins { get { return plugins; } }
        
        public static void LoadPlugins(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                string[] dllFiles = Directory.GetFiles(directoryPath, "*.dll", SearchOption.AllDirectories);
                Parallel.ForEach(dllFiles, file =>
                {
                    LoadPluginFromDll(file);
                });
            } else
            {
                Console.WriteLine("File not found.");
            }
        }

        public static void LoadPluginFromDll(string file)
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(file);
                foreach (Type type in assembly.GetTypes())
                {
                    if(typeof(Plugin).IsAssignableFrom(type))
                    {
                        lock(plugins)
                        {
                            Plugin plugin = (Plugin)Activator.CreateInstance(type);
                            plugins.Add(plugin);
                        }
                    }
                }
            } catch(Exception ex)
            {
                Logger.CoreExcept(ex, "Failed to load plugin from dll file.");
            }
        }
        public static void DisableAll()
        {
            Parallel.ForEach(plugins, plugin =>
            {
                DisablePlugin(plugin);
            });
        }
        public static void EnableAll()
        {
            Parallel.ForEach(plugins, plugin =>
            {
                EnablePlugin(plugin);
            });
        }
        public static void EnablePlugin(Plugin plugin)
        {
            plugin.EnablePlugin();
        }
        public static void DisablePlugin(Plugin plugin)
        {
            plugin.DisablePlugin();
        }
    }
}
