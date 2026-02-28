using System;
using Cysharp.Threading.Tasks;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// View contract for window UI. Handles display, close animation, and lifecycle.
    /// </summary>
    public interface IWindowView
    {
        IWindowPresentationModel PresentationModel { get; }
        event Action OnClosedEvent;

        void Initialize(object args);
        UniTask CloseAsync();
        void Destroy();
    }
}
