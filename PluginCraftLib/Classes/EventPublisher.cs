using PluginCraftLib.Interfaces;

namespace PluginCraftLib.Classes
{
    public class EventPublisher
    {
        private event EventHandler<EventArgs>? EventOccurred;

        public void Subscribe(IEventListener listener)
        {
            EventOccurred += listener.HandleEvent;
        }

        public void Unsubscribe(IEventListener listener)
        {
            EventOccurred -= listener.HandleEvent;
        }

        public void PublishEvent(EventArgs args)
        {
            EventOccurred?.Invoke(this, args);
        }
    }
}