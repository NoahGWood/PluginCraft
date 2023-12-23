using ImGuiNET;
using PluginCraftLib.Interfaces;

namespace GrblControllerPlugin
{
    public class GrblPanel : IPanel
    {
        private bool open = false;
        public bool IsWindowOpen { get => open; set { open = value; } }
        private string customGCode = "";
        private int spindleSpeed = 0;
        public void Render()
        {
            ImGui.Begin("GrblPanel", ref open);

            //ImGui.Columns(4, "JoggingControls", false);
            //ImGui.SetColumnWidth(0, 100); // Adjust column widths as needed
            if(ImGui.TreeNode("State"))
            {
                ImGui.TreePop();
            }
            if (ImGui.TreeNode("Control"))
            {
                ImGui.TreePop();
            }
            if(ImGui.TreeNode("User Commands"))
            {
                ImGui.Button("1");
                ImGui.SameLine();
                ImGui.Button("2");
                ImGui.SameLine();
                ImGui.Button("3");
                ImGui.SameLine();
                ImGui.Button("4");
                ImGui.TreePop();
            }
            if(ImGui.TreeNode("Heightmap"))
            {
                ImGui.TreePop();
            }
            if(ImGui.TreeNode("Spindle"))
            {
                ImGui.Text($"Speed:\t\t{spindleSpeed}");
                ImGui.DragInt("##SpindleSpeed", ref spindleSpeed);
                ImGui.TreePop();
            }
            if(ImGui.TreeNode("Overriding"))
            {
                ImGui.TreePop();
            }
            if (ImGui.TreeNode("Jog"))
            {
                if (ImGui.BeginTable("Jogging Controls", 4))
                {
                    ImGui.TableNextRow();
                    ImGui.TableSetColumnIndex(1);
                    if (ImGui.Button(" Y+"))
                    {
                        SendJogCommand("Y10");
                    }
                    ImGui.TableSetColumnIndex(3);
                    if (ImGui.Button(" Z+ "))
                    {

                    }
                    ImGui.TableNextRow();
                    ImGui.TableSetColumnIndex(0);
                    if (ImGui.Button(" X-"))
                    {
                        SendJogCommand("X-10");
                    }
                    ImGui.TableSetColumnIndex(1);
                    if (ImGui.Button(" H "))
                    {

                    }
                    ImGui.TableSetColumnIndex(2);
                    if (ImGui.Button(" X+"))
                    {
                        SendJogCommand("X10");
                    }
                    ImGui.TableNextRow();
                    ImGui.TableSetColumnIndex(1);
                    if (ImGui.Button(" Y-"))
                    {

                    }
                    ImGui.TableSetColumnIndex(3);
                    if (ImGui.Button(" Z- "))
                    {
                        SendJogCommand("Z-10");
                    }
                    ImGui.EndTable();
                }
                ImGui.Text("Step:");
                ImGui.SameLine();
                if (ImGui.BeginCombo("##ComboStep", "Continuously"))
                {
                    ImGui.EndCombo();
                }
                ImGui.Text("Feed:");
                ImGui.SameLine();
                if (ImGui.BeginCombo("##FeedRate", ""))
                {
                    ImGui.EndCombo();
                }
                ImGui.TreePop();
            }
            ImGui.NewLine();
            ImGui.Separator();
            ImGui.Text("Command: ");
            ImGui.InputText("##GCodeInput", ref customGCode, 256, ImGuiInputTextFlags.EnterReturnsTrue);
            ImGui.SameLine();
            if(ImGui.Button("Send"))
            {
                SendCustomCommand(customGCode);
            }
            ImGui.End();
        }

        public void SendJogCommand(string cmd)
        {

        }
        public void SendCustomCommand(string cmd)
        {

        }
    }
}
