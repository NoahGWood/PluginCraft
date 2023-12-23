
using PluginCraft.Core;
using PluginCraftLib.Classes;
using PluginCraftLib.Interfaces;
using ImGuiNET;
using EZLaunch = EZLauncher.EZLauncher;

namespace PluginCraft.Menus
{
    public class PluginMenu : IMenu
    {
        public List<IMenu> subMenus = new List<IMenu>();
        public void AddMenu(IMenu menu)
        {
            subMenus.Add(menu);
        }
        public void RemoveMenu(IMenu menu)
        {
            subMenus.Remove(menu);
        }
        public void RenderMenu()
        {
            if (ImGui.BeginMenu("Plugins"))
            {
                foreach(Plugin plugin in PluginLoader.Plugins)
                {
                    if(ImGui.BeginMenu(plugin.Name))
                    {
                        if (plugin.IsEnabled)
                        {
                            if (ImGui.MenuItem(plugin.Name))
                            {
                                plugin.Execute();
                            }
                        }
                        if (ImGui.MenuItem("About"))
                        {
                            App.MainWindow.SetPlugin(plugin);
                        }
                        if (ImGui.MenuItem("Help"))
                        {
                            EZLaunch.Launch(plugin.HelpURL);
                        }
                        if (ImGui.MenuItem("Plugin Home"))
                        {
                            EZLaunch.Launch(plugin.ProjectURL);
                        }
                        if (plugin.IsEnabled)
                        {
                            if (ImGui.MenuItem("Disable"))
                            {
                                PluginLoader.DisablePlugin(plugin);
                            }
                        }
                        else
                        {
                            if (ImGui.MenuItem("Enable"))
                            {
                                PluginLoader.EnablePlugin(plugin);
                            }
                        }
                        ImGui.EndMenu();
                    }
                }
                ImGui.EndMenu();
            }
        }
    }
}
