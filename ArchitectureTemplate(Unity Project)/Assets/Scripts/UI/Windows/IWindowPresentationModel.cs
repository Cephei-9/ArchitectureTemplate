using System;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Presentation model for a window. Holds state and logic, disposed when window closes.
    /// </summary>
    public interface IWindowPresentationModel : IDisposable
    {
        void Initialize(object args);
    }
}
