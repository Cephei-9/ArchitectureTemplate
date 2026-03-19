using UnityEngine;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// View implementation for the initialization screen without additional logic.
    /// </summary>
    public class InitializationScreenView : WindowViewBase<InitializationScreenPresenter>
    {
        public override void Close()
        {
            InvokeOnClosedEvent();
        }
    }
}

