using Cysharp.Threading.Tasks;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Service for opening and closing windows. Tracks open windows by id.
    /// </summary>
    public interface IWindowService
    {
        bool IsOpen(WindowId id);
        WindowHandle Open(WindowId id, object args = null);
        UniTask CloseAsync(WindowId id);
    }
}
