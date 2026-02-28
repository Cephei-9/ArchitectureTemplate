using System;
using Cysharp.Threading.Tasks;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Handle for an open window. Provides close API and notifies when window is closed.
    /// </summary>
    public class WindowHandle
    {
        private readonly IWindowView _view;
        private readonly Action _release;

        private bool _isClosing;
        private bool _isReleased;

        public event Action OnClosed;

        public WindowHandle(IWindowView view, Action release)
        {
            _view = view;
            _release = release;
            _view.OnClosedEvent += OnViewClosed;
        }

        public async UniTask CloseAsync()
        {
            if (_isReleased || _isClosing)
                return;

            _isClosing = true;

            try
            {
                await _view.CloseAsync();
            }
            finally
            {
                _isClosing = false;
            }
        }

        private void OnViewClosed()
        {
            if (_isReleased)
                return;

            _isReleased = true;
            _view.OnClosedEvent -= OnViewClosed;
            _release?.Invoke();
            OnClosed?.Invoke();
        }
    }
}
