using System;
using Cysharp.Threading.Tasks;

namespace ArchitectureTemplate.UI
{
    public interface IWindowView
    {
        event Action OnClosedEvent;
        void Close();
        void Destroy();
    }
}