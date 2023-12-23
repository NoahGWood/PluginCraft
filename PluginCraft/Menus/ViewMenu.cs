using PluginCraft.Core;
using PluginCraftLib.Interfaces;
using ImGuiNET;
namespace PluginCraft.Menus
{
    public class ViewMenu : IMenu
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
            if (ImGui.BeginMenu("View"))
            {
                foreach (IMenu menu in subMenus)
                {
                    menu.RenderMenu();
                }
                if(ImGui.MenuItem("About"))
                {
                    App.MainWindow.OpenAbout();
                }
                ImGui.EndMenu();
            }
        }
    }
}
