namespace PluginCraft.Core
{
    using IMenu = PluginCraftLib.Interfaces.IMenu;
    using IPanel = PluginCraftLib.Interfaces.IPanel;
    using ImGui = ImGuiNET.ImGui;
    public static class App
    {
        public static bool EnableLogs = true;
        public static string Name = null;
        public static MainWindow MainWindow;
        public static ISettings Settings;
        public static void CreateApp(string name, int sizeX, int sizeY)
        {
            Name = name;
            MainWindow = new MainWindow(name, sizeX, sizeY);
        }
        public static void AddSettings(ISettings settings)
        {
            Settings = settings;
        }
        public static void AddMenu(IMenu menu)
        {
            MainWindow.AddMenu(menu);
        }
        public static void AddPanel(IPanel panel)
        {
            MainWindow.AddPanel(panel);
        }
        public static void RemoveMenu(IMenu menu)
        {
            MainWindow.RemoveMenu(menu);
        }
        public static void RemovePanel(IPanel panel)
        {
            MainWindow.RemovePanel(panel);
        }
        public static void Start()
        {
            if(Settings != null)
            {
                Settings.LoadSettings(); // For some reason this doesn't work?
            }
            MainWindow.Run();
        }

        public static void Stop()
        {
            if(Settings != null)
            {
                Settings.SaveSettings();
            }
        }

    }
}
