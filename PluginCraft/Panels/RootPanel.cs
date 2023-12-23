using ImGuiNET;
using PluginCraftLib.Interfaces;

namespace PluginCraft.Panels
{
    public class RootPanel : IPanel, IMenu
    {
        private bool isWindowOpen = true;
        public bool IsWindowOpen { get { return isWindowOpen; } set { isWindowOpen = value; } }

        List<IPanel> Panels = new List<IPanel>();
        List<IMenu> Menus = new List<IMenu>();
        public void Render()
        {
            for(int i=0; i<Panels.Count; i++)
            {
                Panels[i].Render();
            }
        }

        public void RenderMenu()
        {
            for(int i=0; i<Menus.Count;i++)
            {
                Menus[i].RenderMenu();
            }
        }

    }
}
