namespace PluginCraftLib.Interfaces
{
    public interface IPanel
    {
        bool IsWindowOpen { get; set; }
        public void Open()
        {
            IsWindowOpen = true;
        }
        public void Close()
        {
            IsWindowOpen = false;
        }
        public void Render();
    }
}
