using PluginCraftLib.Interfaces;
using ImGuiNET;
namespace PluginCraft.Menus
{
    public class EditMenu : IMenu
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
            if (ImGui.BeginMenu("Edit"))
            {
                foreach (IMenu menu in subMenus)
                {
                    menu.RenderMenu();
                }
                ImGui.EndMenu();
            }
        }
    }
}
