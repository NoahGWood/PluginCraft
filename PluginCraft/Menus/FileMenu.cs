using PluginCraft.Core;
using PluginCraftLib.Classes;
using PluginCraftLib.Interfaces;
using ImGuiNET;
using NativeFileDialogSharp;

namespace PluginCraft.Menus
{
    public class FileMenu : IMenu
    {
        public List<IMenu> subMenus = [];
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
            if (ImGui.BeginMenu("File"))
            {
                if(ImGui.BeginMenu("Open"))
                {
                    foreach(Plugin plugin in PluginLoader.Plugins)
                    {
                        if (plugin.IsEnabled)
                        {
                            foreach (IFileHandler<string> handler in plugin.StringFileHandlers)
                            {
                                if (ImGui.MenuItem(handler.FileType))
                                {
                                    string file = Dialog.FileOpen(handler.Extensions).Path;
                                    if (file != null)
                                        handler.ReadFile(file);
                                }
                            }
                            foreach (IFileHandler<byte> handler in plugin.ByteFileHandlers)
                            {
                                if (ImGui.MenuItem(handler.FileType))
                                {
                                    string file = Dialog.FileOpen(handler.Extensions).Path;
                                    if (file != null)
                                        handler.ReadFile(file);
                                }
                            }
                        }
                    }
                    ImGui.EndMenu();
                }
                if (ImGui.BeginMenu("Save"))
                {
                    foreach (Plugin plugin in PluginLoader.Plugins)
                    {
                        if (plugin.IsEnabled)
                        {
                            foreach (IFileHandler<string> handler in plugin.StringFileHandlers)
                            {
                                if (ImGui.MenuItem(handler.FileType))
                                {
                                    string file = Dialog.FileSave(handler.Extensions).Path;
                                    if (file != null)
                                        handler.SaveFile(file);
                                }
                            }
                            foreach (IFileHandler<byte> handler in plugin.ByteFileHandlers)
                            {
                                if (ImGui.MenuItem(handler.FileType))
                                {
                                    string file = Dialog.FileSave(handler.Extensions).Path;
                                    if (file != null)
                                        handler.SaveFile(file);
                                }
                            }
                        }
                    }
                    ImGui.EndMenu();
                }
                foreach (IMenu menu in subMenus)
                {
                    menu.RenderMenu();
                }
                ImGui.Separator();
                if(ImGui.MenuItem("Exit", "Alt+F4"))
                {
                    App.Stop();
                    App.MainWindow.Close();
                }
                ImGui.EndMenu();
            }
        }
    }
}
