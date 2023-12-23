using ImGuiNET;
using PluginCraftLib.Interfaces;
using EZLaunch = EZLauncher.EZLauncher;
using PluginCraftLib.Classes;

namespace PluginCraft.Panels
{
    public class PluginPanel : IPanel
    {
        private Plugin plugin;
        private bool isWindowOpen = false;
        public bool IsWindowOpen { get { return isWindowOpen; } set { isWindowOpen = value; } }

        public void SetPlugin(Plugin? plugin)
        {
            this.plugin = plugin;
            isWindowOpen = true;
        }
        public void Render()
        {
            if (isWindowOpen)
            {
                ImGui.Begin("Plugin Info", ref isWindowOpen);
                ImGui.Text("Plugin: ");
                ImGui.SameLine();
                ImGui.Text((plugin != null) ? plugin.Name : "");
                ImGui.Text("Version: ");
                ImGui.SameLine();
                ImGui.Text((plugin != null) ? plugin.Version : "");
                ImGui.Text("Description: ");
                ImGui.TextWrapped((plugin != null) ? plugin.Description : "");
                if(ImGui.Button("Website"))
                {
                    if (plugin != null && plugin.ProjectURL != null)
                        EZLaunch.Launch(plugin.ProjectURL);
                }
                if (ImGui.Button("Documentation"))
                {
                    if (plugin != null && plugin.HelpURL != null)
                        EZLaunch.Launch(plugin.HelpURL);
                }
                ImGui.End();
            }
        }
    }
}
