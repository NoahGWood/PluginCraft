using StbImageSharp;
using OpenTK.Graphics.OpenGL4;
using ImGuiNET;
using System.Numerics;

namespace PluginCraft.Core
{
    public class Texture2D
    {
        public int TextureID { get; private set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public Texture2D() { }
        public Texture2D(string fileName)
        {
            Load(fileName);
        }
        public Texture2D(ImageResult data)
        {
            Load(data);
        }
        ~Texture2D()
        {
            GL.DeleteTexture(TextureID);
        }
        public void Load(string fileName)
        {
            GL.ClearColor(0f,0f, 0f, 1f);
            GL.Enable(EnableCap.Texture2D);
            LoadTexture(fileName);
        }
        public void Load(ImageResult data)
        {
            GL.ClearColor(0f, 0f, 0f, 1f);
            GL.Enable(EnableCap.Texture2D);
            CreateTexture(data);
        }
        public void DrawImage()
        {
            if(TextureID != null)
            {
                ImGui.Image((IntPtr)TextureID, new Vector2(this.Width, this.Height));
            }
        }
        public void DrawImage(Vector4 color)
        {
            if (TextureID != null)
            {
                ImGui.Image((IntPtr)TextureID, new Vector2(Width, Height), new Vector2(0f, 1f), new Vector2(1f, 0f), color);
            }
        }
        public void DrawImage(Vector4 color, Vector4 border)
        {
            if (TextureID != null)
            {
                ImGui.Image((IntPtr)TextureID, new Vector2(Width, Height), new Vector2(0f, 1f), new Vector2(1f, 0f), color, border);
            }
        }
        public bool DrawButton(string id)
        {
            return ImGui.ImageButton(id, (IntPtr)TextureID, new Vector2(Width, Height));
        }
        private void LoadTexture(string fileName)
        {
            if (File.Exists(fileName))
            {
                byte[] data = File.ReadAllBytes(fileName);
                ImageResult res = ImageResult.FromMemory(data, ColorComponents.RedGreenBlueAlpha);
                CreateTexture(res);
            } else
            {
                LoadTextureFromURL(fileName);
            }
        }
        private void LoadTextureFromURL(string imageUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    byte[] imageData = client.GetByteArrayAsync(imageUrl).Result;
                    ImageResult res = ImageResult.FromMemory(imageData, ColorComponents.RedGreenBlueAlpha);
                    CreateTexture(res);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error downloading image {e.Message}");
                }
            }
        }
        public void CreateTexture(ImageResult res)
        {
            this.Width = res.Width;
            this.Height = res.Height;
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);
            TextureID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, TextureID);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, res.Data);
        }
    }
}
