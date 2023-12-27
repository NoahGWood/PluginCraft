using ImGuiNET;
using PluginCraftLib.Interfaces;
using NativeFileDialogSharp;
using System.Numerics;
using PluginCraft.Core;
namespace GrblControllerPlugin
{
    public class GrblViewerPanel : IPanel
    {
        public static GrblDevice ActiveDevice;
        private bool open = true;
        public bool IsWindowOpen { get => open; set
            {
                open = value;
            }
            }

        private List<(float x, float y, float z)> pathPoints = [];
        Texture2D texture;

        public void Render()
        {
            if (IsWindowOpen)
            {
                ImGui.Begin("GRBL Viewer", ref open);
                if (ActiveDevice != null)
                {
                    ImGui.Text(ActiveDevice.Name);

                }
                if(ImGui.Button("Test"))
                {
                    pathPoints = new List<(float x, float y, float z)>
        {
            (0, 0, 0),      // Starting point
            (75, 0, 0),     // Middle of the base
            (150, 150, 0),  // Top right corner
            (0, 150, 0),    // Top left corner
            (0, 0, 0)       // Back to the starting point
        };

                    texture = new Texture2D();
                    texture.CreateFromPoints(300, 300, pathPoints);
                }
                if (ImGui.Button("Open GCode"))
                {
                    string f = Dialog.FileOpen().Path;
                    if(f != "")
                        LoadGCodeFile(f);
                }
                if(pathPoints.Count > 0)
                {
                    DrawGCodePath();
                }
                ImGui.End();
            }
        }
        public void LoadGCodeFile(string path)
        {
            pathPoints.Clear();
            try
            {
                string[] lines = File.ReadAllLines(path);
                foreach(string line in lines)
                {
                    ParseGCode(line);
                }
            } catch (Exception ex)
            {
                Console.WriteLine($"Error reading GCode file: {ex.Message}");
            }
            int maxX = 300;
            int maxY = 300;
            foreach(var p in pathPoints)
            {
                if (p.x > maxX/2)
                    maxX = (int)p.x * 2;
                if (p.y > maxY/2)
                    maxY = (int)p.y * 2;
            }
            texture = new Texture2D();
            texture.CreateFromPoints(maxY, maxX, pathPoints);
        }

        private void ParseGCode(string line)
        {
            if(line.StartsWith("G1") || line.StartsWith("G2"))
            {
                float x = GetCoordinateValue(line, 'X');
                float y = GetCoordinateValue(line, 'Y');
                float z = GetCoordinateValue(line, 'Z');

                // Extract I, J, K coordinates for arcs
                float i = GetCoordinateValue(line, 'I');
                float j = GetCoordinateValue(line, 'J');
                float k = GetCoordinateValue(line, 'K');

                pathPoints.Add((x, y, z));
                // Check if the line represents an arc
                if (i!=0 || j!=0 || k!=0)
                {
                    Console.WriteLine($"Arc detected: I={i}, J={j}, K={k}");

                    // Convert arc to points
                    List<(float x, float y, float z)> arcPoints = ConvertArcToPoints(pathPoints[pathPoints.Count - 1], i, j, k, 10);
                    pathPoints.AddRange(arcPoints);
                }

            }
        }

        private float GetCoordinateValue(string line, char coordinate)
        {
            int index = line.IndexOf(coordinate);
            if (index == -1)
                return 0.0f;
            int endIndex = line.IndexOf(' ', index);
            string v = endIndex == -1 ? line.Substring(index + 1) : line.Substring(index + 1, endIndex - index - 1);

            if (float.TryParse(v, out float result))
                return result;
            return 0.0f;
        }

        private void DrawGCodePath()
        {
            if(texture.TextureID != 0 && texture.TextureID != null)
                texture.DrawImage();
        }

        private static List<(float x, float y, float z)> ConvertArcToPoints((float x, float y, float z) startPoint, float i, float j, float k, int segments)
        {
            List<(float x, float y, float z)> arcPoints = new List<(float x, float y, float z)>();

            // Calculate the radius and center coordinates
            float radius = (float)Math.Sqrt(Math.Pow(i, 2) + Math.Pow(j, 2) + Math.Pow(k, 2));
            float centerX = startPoint.x + i;
            float centerY = startPoint.y + j;

            // Calculate start and end angles
            float startAngle = (float)Math.Atan2(startPoint.y - centerY, startPoint.x - centerX);
            float endAngle = (float)Math.Atan2(k, i);

            // Ensure the end angle is positive
            if (endAngle < 0)
            {
                endAngle += 2 * (float)Math.PI;
            }

            // Calculate the angle increment for each segment
            float angleIncrement = (endAngle - startAngle) / segments;

            // Generate points along the arc
            for (int s = 0; s <= segments; s++)
            {
                float angle = startAngle + s * angleIncrement;
                float x = centerX + radius * (float)Math.Cos(angle);
                float y = centerY + radius * (float)Math.Sin(angle);
                arcPoints.Add((x, y, startPoint.z)); // Assume Z-coordinate remains the same for simplicity
            }

            return arcPoints;
        }
    }
}
