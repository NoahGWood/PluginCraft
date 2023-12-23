using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using ImGuiNET;
using System.ComponentModel;
using PluginCraftLib.Interfaces;
using PluginCraft.Menus;
using PluginCraft.Panels;
using PluginCraftLib.Classes;

namespace PluginCraft.Core
{
    public class MainWindow : GameWindow
    {
        private float m_Width;
        private float m_Height;

        private List<IPanel> m_Panels;
        private List<IMenu> m_Menus;

        private List<ICommand> m_Commands = new List<ICommand>();
        private List<EventPublisher> m_Events = new List<EventPublisher>();

        private int m_FramebufferTexture;
        private int m_RectVAO;
        public ImGuiController ImGuiController;

        private static FileMenu fileMenu = new();
        private static EditMenu editMenu = new();
        private static ViewMenu viewMenu = new();
        private static PluginMenu pluginMenu = new();
        private static AboutPanel aboutPanel = new();
        private static PluginPanel pluginPanel = new();
        public MainWindow(string title, int  width, int height)
            : base(GameWindowSettings.Default, new NativeWindowSettings()
            {
                Title = title,
                Size = new Vector2i(width, height),
                StartVisible = true,
                StartFocused = true,
                WindowState = WindowState.Normal,
                API = ContextAPI.OpenGL,
                Profile = ContextProfile.Core,
                APIVersion = new Version(3,3)
            })
       {
            CenterWindow();
            m_Height = Size.Y;
            m_Width = Size.X;

            m_Panels = new List<IPanel>();
            m_Menus = new List<IMenu>();
            AddMenu(fileMenu);
            AddMenu(editMenu);
            AddMenu(viewMenu);
            AddMenu(pluginMenu);
            AddPanel(aboutPanel);
            AddPanel(pluginPanel);
            PluginLoader.LoadPlugins("Plugins");
        }
    
        public void AddPanel(IPanel p) { m_Panels.Add(p); }
        public void RemovePanel(IPanel p) { m_Panels.Remove(p); }
        public void AddMenu(IMenu p) {  m_Menus.Add(p); }
        public void RemoveMenu(IMenu p) { m_Menus.Remove(p); }
        public void AddCommand(ICommand command) { m_Commands.Add(command); }
        public void RemoveCommand(ICommand command) { m_Commands.Remove(command); }
        public void AddEventPublisher(EventPublisher eventPublisher) { m_Events.Add(eventPublisher); }
        public void RemoveEventPublisher(EventPublisher eventPublisher) { m_Events.Remove(eventPublisher); }
        public void SetPlugin(Plugin p)
        {
            pluginPanel.SetPlugin(p);
        }
        public void OpenAbout()
        {
            ((IPanel)aboutPanel).Open();
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            foreach(Plugin plugin in PluginLoader.Plugins)
            {
                if(plugin.IsEnabled)
                {
                    plugin.Update((long)args.Time);
                }
            }
        }
        protected override void OnLoad()
        {
            m_FramebufferTexture = GL.GenTexture();
            GL.ClearColor(new Color4(0.5f, 0.5f, 0.5f, 1.0f));
            GL.LineWidth(2.0f);
            GL.PointSize(5f);
            ImGuiController = new ImGuiController(ClientSize.X, ClientSize.Y);
            OpenGLRenderer.CheckGLError("OnLoad");

            base.OnLoad();
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            m_Height = e.Height;
            m_Width = e.Width;

            GL.DeleteFramebuffer(m_FramebufferTexture);
            GL.Viewport(0,0, ClientSize.X, ClientSize.Y);
            ImGuiController.WindowResized(ClientSize.X, ClientSize.Y);
            base.OnResize(e);
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            ImGuiController.Update(this, (float)args.Time);

            ImGui.DockSpaceOverViewport();

            if (ImGui.BeginMainMenuBar())
            {
                foreach (IMenu menu in m_Menus)
                {
                    menu.RenderMenu();
                }
                Parallel.ForEach(PluginLoader.Plugins, plugin =>
                {
                    if (plugin.IsEnabled && plugin.Menus != null)
                    {
                        foreach (IMenu menu in plugin.Menus)
                        {
                            menu.RenderMenu();
                        }
                    }
                });
                ImGui.EndMainMenuBar();
            }

            foreach (IPanel panel in m_Panels)
            {
                panel.Render();
            }
            Parallel.ForEach(PluginLoader.Plugins, plugin =>
            {
                if (plugin.IsEnabled && plugin.Panels != null)
                {
                    foreach (IPanel panel in plugin.Panels)
                    {
                        panel.Render();
                    }
                }
            });

            ImGuiController.Render();
            
            OpenGLRenderer.CheckGLError("End of frame");

            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnTextInput(TextInputEventArgs e)
        {
            base.OnTextInput(e);
            ImGuiController.PressChar((char)e.Unicode);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            ImGuiController.MouseScroll(e.Offset);
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            App.Stop();
            base.OnClosing(e);
        }
    }
}
