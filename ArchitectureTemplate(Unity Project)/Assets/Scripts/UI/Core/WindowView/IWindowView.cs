using System;
using Cysharp.Threading.Tasks;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// View contract for window UI. Handles closing and destruction notifications.
    /// </summary>
    public interface IWindowView
    {
        event Action OnClosedEvent;
        void Close();
        void Destroy();
    }
}