using PluginCraftLib.Classes;

namespace CADPlugin
{
    public class CadPlugin : Plugin
    {
        public override string Name => "CAD Plugin";

        public override string? Description => "CAD functionality for the PluginCraft app.";

        public override string? ProjectURL => "https://github.com/NoahGWood/PluginCraft";

        public override string? HelpURL => "https://github.com/NoahGWood/PluginCraft";

        public override string? Author => "NoahGWood";

        public override string? Version => "1.0.0";

        public override string? UpdateURL => "https://github.com/NoahGWood/PluginCraft";

        public override string? License => "https://github.com/NoahGWood/PluginCraft/LICENSE.md";

        private bool enabled = true;
        public override bool IsEnabled
        {
            get => enabled; set
            {
                enabled = value;
            }
        }
        public CadPlugin()
        {
            StringFileHandlers.Add(new CADFileHandler());
        }
        public override void DisablePlugin()
        {
        }

        public override void EnablePlugin()
        {
        }

        public override void Execute()
        {
        }

        public override void Update(long tick)
        {
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
    }
}
