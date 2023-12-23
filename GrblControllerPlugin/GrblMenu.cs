using ImGuiNET;
using PluginCraftLib.Interfaces;

namespace GrblControllerPlugin
{
    public class GrblMenu : IMenu
    {
        public void RenderMenu()
        {
            if (ImGui.BeginMenu("Device"))
            {
                if(ImGui.MenuItem("Add Grbl Device"))
                {
                    Console.WriteLine("Adding GRBL Device");
                }
                ImGui.EndMenu();
            }
        }
    }
}
