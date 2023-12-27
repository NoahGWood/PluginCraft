using PluginCraftLib.Interfaces;

namespace PluginCraftLib.Classes
{
    public delegate void PluginEventHandler(Plugin plugin);

    public abstract class Plugin
    {
        public abstract string Name { get; }
        public abstract string? Description { get; }
        public abstract string? ProjectURL { get; }
        public abstract string? HelpURL { get; }
        public abstract string? Author { get; }
        public abstract string? Version { get; }
        public abstract string? UpdateURL { get; }
        public abstract string? License { get; }
        public abstract bool IsEnabled { get; set; }
        // List of interfaces
        public List<ICommand>? Commands { get; protected set; } = [];
        public List<IEventListener>? EventListeners { get; protected set; } = [];
        public List<IFileHandler<string>>? StringFileHandlers { get; protected set; } = [];
        public List<IFileHandler<byte>>? ByteFileHandlers { get; protected set; } = [];
        public List<IMenu>? Menus { get; protected set; } = [];
        public List<IPanel>? Panels { get; protected set; } = [];
        public List<ISerialDevice>? SerialDevices { get; protected set; } = [];
        public List<ISettings>? Settings { get; protected set; } = [];
        public abstract void EnablePlugin();
        public abstract void DisablePlugin();
        public abstract void Execute();
        public abstract void Update(long tick);
        protected abstract void LogError(string message);
        protected abstract void LogWarning(string message);
        protected abstract void LogInfo(string message);
    }
}
