using ImGuiNET;
using PluginCraftLib.Classes;
using PluginCraftLib.Interfaces;
using System.Linq;

namespace GrblControllerPlugin
{
    public class GrblPanel : IPanel
    {
        public bool IsWindowOpen { get => open; set { open = value; } }
        private Dictionary<string, GrblDevice> devices = [];
        private bool open = true;
        private string customGCode = "";
        private int spindleSpeed = 0;
        public static bool ShowAddModel = false;

        private int jogSpeed;
        private float jogStep;
        public void Render()
        {
            if (IsWindowOpen)
            {
                ImGui.Begin("GrblPanel", ref open);

                if (ImGui.Button("Add GRBL Device"))
                {
                    ShowAddModel = true;
                }
                ImGui.SeparatorText("Status");
                if (ImGui.BeginChild("Status", new System.Numerics.Vector2(0,150), ImGuiChildFlags.Border |ImGuiChildFlags.ResizeY))
                {
                    foreach(KeyValuePair<string, GrblDevice> kval in devices)
                    {
                        ImGui.Text(kval.Value.Name);
                        ImGui.SameLine();
                        ImGui.Text("Status");
                    }
                    ImGui.EndChild();
                }
                ImGui.SeparatorText("Devices");
                foreach (KeyValuePair<string, GrblDevice> kval in devices)
                {
                    GrblDevice device = kval.Value;
                    if (ImGui.TreeNode(device.Name))
                    {
                        if (GrblViewerPanel.ActiveDevice != null)
                        {
                            if(GrblViewerPanel.ActiveDevice != device)
                                if(ImGui.Button("Set Active"))
                                    GrblViewerPanel.ActiveDevice = device;
                        } else
                            GrblViewerPanel.ActiveDevice = device;
                        if (ImGui.TreeNode("Settings"))
                        {
                            string o = device.Name;
                            if (ImGui.InputText("Name", ref o, 60))
                            {
                                device.Name = o;
                            }

                            bool lmode = device.GrblSettings.LaserMode || false;
                            if (ImGui.Checkbox("Laser Mode", ref lmode))
                                device.GrblSettings.SetSetting(32, lmode);
                            if(ImGui.TreeNode("Spindle"))
                            {
                                float maxs = device.GrblSettings.MaxSpindleSpeedRPM;
                                if (ImGui.InputFloat("Max Spindle Speed, RPM", ref maxs))
                                    device.GrblSettings.SetSetting(30, maxs);
                                float mins = device.GrblSettings.MinSpindleSpeedRPM;
                                if (ImGui.InputFloat("Min Spindle Speed, RPM", ref mins))
                                    device.GrblSettings.SetSetting(31, mins);
                                ImGui.TreePop();
                            }
                            if (ImGui.TreeNode("Step"))
                            {
                                float xsmm = device.GrblSettings.XStepsMM;
                                if (ImGui.InputFloat("X Steps, MM", ref xsmm))
                                    device.GrblSettings.SetSetting(100, xsmm);
                                float ysmm = device.GrblSettings.YStepsMM;
                                if (ImGui.InputFloat("Y Steps, MM", ref ysmm))
                                    device.GrblSettings.SetSetting(101, ysmm);
                                float zsmm = device.GrblSettings.ZStepsMM;
                                if (ImGui.InputFloat("Z Steps, MM", ref zsmm))
                                    device.GrblSettings.SetSetting(102, zsmm);

                                float xmr = device.GrblSettings.XMaxRateMM;
                                if (ImGui.InputFloat("X Max Rate, MM", ref xmr))
                                    device.GrblSettings.SetSetting(110, xmr);
                                float ymr = device.GrblSettings.YMaxRateMM;
                                if (ImGui.InputFloat("Y Max Rate, MM", ref ymr))
                                    device.GrblSettings.SetSetting(111, ymr);
                                float zmr = device.GrblSettings.ZMaxRateMM;
                                if (ImGui.InputFloat("Z Max Rate, MM", ref zmr))
                                    device.GrblSettings.SetSetting(112, zmr);

                                float xamm = device.GrblSettings.XAccelMM;
                                if (ImGui.InputFloat("X Acceleration, MM", ref xamm))
                                    device.GrblSettings.SetSetting(120, xamm);
                                float yamm = device.GrblSettings.YAccelMM;
                                if (ImGui.InputFloat("Y Acceleration, MM", ref yamm))
                                    device.GrblSettings.SetSetting(121, yamm);
                                float zamm = device.GrblSettings.ZAccelMM;
                                if (ImGui.InputFloat("Z Acceleration, MM", ref zamm))
                                    device.GrblSettings.SetSetting(122, zamm);

                                int sp = device.GrblSettings.StepPulse;
                                if (ImGui.InputInt("Step Pulse", ref sp))
                                    device.GrblSettings.SetSetting(0, sp);
                                int sid = device.GrblSettings.StepIdleDelay;
                                if (ImGui.InputInt("Step Idle Delay", ref sid))
                                    device.GrblSettings.SetSetting(1, sid);
                                int spim = device.GrblSettings.StepPortInvertMask;
                                if (ImGui.InputInt("Step Port Invert Mask", ref spim))
                                    device.GrblSettings.SetSetting(2, spim);
                                bool sei = device.GrblSettings.StepEnableInvert;
                                if (ImGui.Checkbox("Step Enable Invert", ref sei))
                                    device.GrblSettings.SetSetting(4, sei);
                                ImGui.TreePop();
                            }
                            if (ImGui.TreeNode("Limits"))
                            {
                                bool lpi = device.GrblSettings.LimitPinsInvert;
                                if (ImGui.Checkbox("Limit Pins Invert", ref lpi))
                                    device.GrblSettings.SetSetting(5, lpi);
                                bool sl = device.GrblSettings.SoftLimits;
                                if (ImGui.Checkbox("Soft Limits", ref sl))
                                    device.GrblSettings.SetSetting(20, sl);
                                bool hl = device.GrblSettings.HardLimits;
                                if (ImGui.Checkbox("Hard Limits", ref hl))
                                    device.GrblSettings.SetSetting(21, hl);
                                ImGui.TreePop();
                            }
                            if(ImGui.TreeNode("Homing"))
                            {
                                float hfmm = device.GrblSettings.HomingFeedMM;
                                if (ImGui.InputFloat("Homing Feed, MM", ref hfmm))
                                    device.GrblSettings.SetSetting(24, hfmm);
                                float hsmm = device.GrblSettings.HomingSeekMM;
                                if (ImGui.InputFloat("Homing Seek, MM", ref hsmm))
                                    device.GrblSettings.SetSetting(25, hsmm);
                                float hpmm = device.GrblSettings.HomingPulloffMM;
                                if (ImGui.InputFloat("Homing Pulloff, MM", ref hpmm))
                                    device.GrblSettings.SetSetting(27, hpmm);
                                ImGui.TreePop();
                            }
                            if (ImGui.TreeNode("Other"))
                            { 
                                int dpim = device.GrblSettings.DirectionPortInvertMask;
                                if (ImGui.InputInt("Direction Port Invert Mask", ref dpim))
                                    device.GrblSettings.SetSetting(3, dpim);
                                int srm = device.GrblSettings.StatusReportMask;
                                if (ImGui.InputInt("Status Report Mask", ref srm))
                                    device.GrblSettings.SetSetting(10, srm);
                                int hdi = device.GrblSettings.HomingDirInvert;
                                if (ImGui.InputInt("Homing Dir Invert", ref hdi))
                                    device.GrblSettings.SetSetting(23, hdi);
                                int hdms = device.GrblSettings.HomingDebounceMS;
                                if (ImGui.InputInt("Homing Debounce MS", ref hdms))
                                    device.GrblSettings.SetSetting(26, hdms);

                                bool ppi = device.GrblSettings.ProbePinInvert;
                                if (ImGui.Checkbox("Probe Pin Invert", ref ppi))
                                    device.GrblSettings.SetSetting(6, ppi);
                                bool hmc = device.GrblSettings.HomingCycle;
                                if (ImGui.Checkbox("Homing Cycle", ref hmc))
                                    device.GrblSettings.SetSetting(22, hmc);
                                bool rpi = device.GrblSettings.ReportInches;
                                if (ImGui.Checkbox("Reoprt Inches", ref rpi))
                                    device.GrblSettings.SetSetting(13, rpi);
                                ImGui.TreePop();
                            }
                            ImGui.TreePop();
                        }
                        if (ImGui.TreeNode("State"))
                        {
                            ImGui.TreePop();
                        }
                        if (ImGui.TreeNode("Control"))
                        {
                            ImGui.TreePop();
                        }
                        if (ImGui.TreeNode("User Commands"))
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
                        if (ImGui.TreeNode("Heightmap"))
                        {
                            ImGui.TreePop();
                        }
                        if (ImGui.TreeNode("Spindle"))
                        {
                            ImGui.Text($"Speed:\t\t{spindleSpeed}");
                            ImGui.DragInt("##SpindleSpeed", ref spindleSpeed);
                            ImGui.TreePop();
                        }
                        if (ImGui.TreeNode("Overriding"))
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
                                    device.Jog("Y", jogStep, jogSpeed);
                                }
                                ImGui.TableSetColumnIndex(3);
                                if (ImGui.Button(" Z+ "))
                                {
                                    device.Jog("Z", jogStep, jogSpeed);
                                }
                                ImGui.TableNextRow();
                                ImGui.TableSetColumnIndex(0);
                                if (ImGui.Button(" X-"))
                                {
                                    device.Jog("X", -jogStep, jogSpeed);
                                }
                                ImGui.TableSetColumnIndex(1);
                                if (ImGui.Button(" H "))
                                {
                                    device.Home();
                                }
                                ImGui.TableSetColumnIndex(2);
                                if (ImGui.Button(" X+"))
                                {
                                    device.Jog("X", jogStep, jogSpeed);
                                }
                                ImGui.TableNextRow();
                                ImGui.TableSetColumnIndex(1);
                                if (ImGui.Button(" Y-"))
                                {
                                    device.Jog("Y", -jogStep, jogSpeed);
                                }
                                ImGui.TableSetColumnIndex(3);
                                if (ImGui.Button(" Z- "))
                                {
                                    device.Jog("Z", -jogStep, jogSpeed);
                                }
                                ImGui.EndTable();
                            }
                            ImGui.Text("Step:");
                            ImGui.SameLine();
                            ImGui.InputFloat("##Step", ref jogStep);

                            ImGui.Text("Feed:");
                            ImGui.SameLine();
                            ImGui.InputInt("##Feed", ref jogSpeed);
                            ImGui.TreePop();
                        }
                        ImGui.NewLine();
                        ImGui.SeparatorText("Commands");
                        bool sendCmd = ImGui.InputText("##GCodeInput", ref customGCode, 256, ImGuiInputTextFlags.EnterReturnsTrue);
                        ImGui.SameLine();
                        if (ImGui.Button("Send") || sendCmd)
                        {
                            Console.WriteLine($"Sending Command: {customGCode}");
                            device.WriteData(customGCode);
                        }
                        if (ImGui.BeginChild("##data", new System.Numerics.Vector2(0, 50), ImGuiChildFlags.Border | ImGuiChildFlags.ResizeY))
                        {
                            if (ImGui.Button("Clear"))
                                device.AllData = "";
                            ImGui.TextWrapped(device.AllData);
                            ImGui.EndChild();
                        }
                        ImGui.TreePop();
                    } else
                    {
                        if (GrblViewerPanel.ActiveDevice == device)
                            GrblViewerPanel.ActiveDevice = null;
                    }
                }
                ImGui.End();
            }
            SelectGrblDevicePopup();
            if (ShowAddModel)
                ImGui.OpenPopup("Select GRBL Device");
        }
        private string m_PortName = "";
        private void SelectGrblDevicePopup()
        {
            bool foundPorts = false;
            if (ImGui.BeginPopupModal("Select GRBL Device"))
            {
                if (ImGui.BeginCombo("Select Port", m_PortName))
                {
                    foreach (string port in SerialHelper.AvailablePorts)
                    {
                        if(!devices.ContainsKey(port))
                        {
                            foundPorts = true;
                            bool isSelected = (m_PortName == port);
                            if(ImGui.Selectable(port, isSelected))
                            {
                                m_PortName = port;
                            }
                            if (isSelected)
                                ImGui.SetItemDefaultFocus();
                        }
                    }
                    if(!foundPorts)
                    {
                        ImGui.Selectable("No Ports Found.", false, ImGuiSelectableFlags.Disabled);
                    }
                    ImGui.EndCombo();
                }
                if(ImGui.Button("Cancel"))
                {
                    m_PortName = "";
                    ShowAddModel = false;
                    ImGui.CloseCurrentPopup();
                }
                ImGui.SameLine();
                if(ImGui.Button("Add Device"))
                {
                    if(m_PortName != "")
                    {
                        GrblDevice dev = new GrblDevice(m_PortName, 115200);
                        devices.Add(m_PortName, dev);
                    }
                    m_PortName = "";
                    ShowAddModel = false;
                    ImGui.CloseCurrentPopup();
                }
                ImGui.EndPopup();
            }

        }
    }
}
