using ImGuiNET;
using PluginCraftLib.Interfaces;

namespace PluginCraft.Panels
{
    public class AboutPanel : IPanel
    {
        private bool isWindowOpen = true;
        public bool IsWindowOpen {  get { return isWindowOpen; } set { isWindowOpen = value; } }

        public void Render()
        {
            if(isWindowOpen)
            {
                ImGui.Begin("About", ref isWindowOpen);
                ImGui.Text("This is an about panel.");
                ImGui.End();
            }
        }
    }
}
