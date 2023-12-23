using PluginCraftLib.Classes;

namespace PluginCraftLib.Interfaces
{
    public interface IEventListener
    {
        void HandleEvent(object? sender, EventArgs? e);

        // Helper functions - Don't change these in your plugins.
        EventPublisher Publisher { get; set; }
        void Subscribe()
        {
            Publisher.Subscribe(this);
        }
        void Subscribe(EventPublisher publisher)
        {
            Publisher = publisher;
            Publisher.Subscribe(this);
        }
        void Unsubscribe()
        {
            Publisher.Unsubscribe(this);
        }
    }
}
