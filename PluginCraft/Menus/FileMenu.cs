using PluginCraft.Core;
using PluginCraftLib.Interfaces;
using ImGuiNET;

namespace PluginCraft.Menus
{
    public class FileMenu : IMenu
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
            if (ImGui.BeginMenu("File"))
            {
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
