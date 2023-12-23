using PluginCraftLib.Classes;

namespace GrblControllerPlugin
{
    public class GrblControllerPlugin : Plugin
    {
        public override string Name => "GrblController";

        public override string? Description => "GrblController is a basic GRBL based controller for the PluginCraft platform.";

        public override string? ProjectURL => "https://github.com/NoahGWood/PluginCraft";

        public override string? HelpURL => "https://github.com/NoahGWood/PluginCraft";

        public override string? Author => "NoahGWood";

        public override string? Version => "1.0.0";

        public override string? UpdateURL => "https://github.com/NoahGWood/PluginCraft";

        public override string? License => "https://github.com/NoahGWood/PluginCraft/LICENSE.md";

        private bool enabled = true;
        public override bool IsEnabled { get => enabled; set {
                enabled = value;
            } }

        public GrblControllerPlugin()
        {
            Menus.Add(new GrblMenu());
            Panels.Add(new GrblPanel());
        }
        public override void DisablePlugin()
        {
            enabled = false;
            foreach(var item in Panels)
            {
                item.IsWindowOpen = false;
            }
        }

        public override void EnablePlugin()
        {
            enabled = true;
            foreach (var item in Panels)
            {
                item.IsWindowOpen = true;
            }
        }

        public override void Execute()
        {
            Console.WriteLine("Executed.");
        }

        protected override void LogError(string message)
        {
        }

        protected override void LogInfo(string message)
        {
        }

        protected override void LogWarning(string message)
        {
        }

        public override void Update(long tick)
        {

        }
    }
}
