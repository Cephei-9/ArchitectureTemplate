using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Manages open windows. Subscribes to handle close events to remove from tracking.
    /// </summary>
    public class WindowService
    {
        private readonly WindowFactory _factory;
        private readonly Dictionary<WindowId, WindowHandle> _openMap = new();

        public WindowService(WindowFactory factory)
        {
            _factory = factory;
        }

        public bool IsOpen(WindowId id)
        {
            return _openMap.ContainsKey(id);
        }

        public WindowHandle Open(WindowId id, object args = null)
        {
            if (_openMap.TryGetValue(id, out WindowHandle existing))
                return existing;

            WindowHandle handle = _factory.Create(id, args);
            _openMap[id] = handle;
            handle.OnClosed += () => _openMap.Remove(id);

            return handle;
        }

        public UniTask CloseAsync(WindowId id)
        {
            if (_openMap.TryGetValue(id, out WindowHandle handle))
                return handle.CloseAsync();

            return UniTask.CompletedTask;
        }
    }
}
