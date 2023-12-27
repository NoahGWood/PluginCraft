using StbImageSharp;
using OpenTK.Graphics.OpenGL4;
using ImGuiNET;
using System.Numerics;
using SkiaSharp;
using System.Runtime.InteropServices;

namespace PluginCraft.Core
{
    public class Texture2D
    {
        public int TextureID { get; private set; } = -1;
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
            if(TextureID >= 0)
            {
                ImGui.Image((IntPtr)TextureID, new Vector2(this.Width, this.Height));
            }
        }
        public void DrawImage(Vector4 color)
        {
            if (TextureID >= 0)
            {
                ImGui.Image((IntPtr)TextureID, new Vector2(Width, Height), new Vector2(0f, 1f), new Vector2(1f, 0f), color);
            }
        }
        public void DrawImage(Vector4 color, Vector4 border)
        {
            if (TextureID >= 0)
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
        public void CreateTexture(int width, int height, byte[] data)
        {
            this.Width = width;
            this.Height = height;
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);
            TextureID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, TextureID);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, data);
        }

        public void CreateFromPoints(int width, int height, List<(float x, float y, float z)> points)
        {

            GL.ClearColor(0f, 0f, 0f, 1f);
            //GL.Enable(EnableCap.Texture2D);

            this.Width = width;
            this.Height = height;
            var bitmap = new SKBitmap(width, height);
            var canvas = new SKCanvas(bitmap);

            canvas.Clear(SKColors.Black);
            var paint = new SKPaint();
            paint.Color = SKColors.White;
            paint.StrokeWidth = 2;
            for (int i = 0; i < points.Count - 1; i++)
            {
                canvas.DrawLine(points[i].x + width/2, points[i].y + height/2, points[i + 1].x + width / 2, points[i + 1].y + height/ 2, paint);
            }
            int last = points.Count-1;
            canvas.DrawLine(points[last].x + width / 2, points[last].y + height / 2, points[last - 1].x + width / 2, points[last - 1].y + height / 2, paint);
            canvas.Flush();

            TextureID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, TextureID);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, Width, Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, bitmap.Bytes);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);
        }
    }
}
