using System;
using System.Threading;
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
        private readonly CancellationTokenSource _cts;

        private AsyncLazy _closeTask;

        public WindowHandle(IWindowView view, CancellationTokenSource cts, Action release)
        {
            _cts = cts;
            _view = view;
            _release = release;
            _view.OnClosedEvent += OnViewClosed;
        }

        public event Action OnClosedEvent;
        
        public bool IsClose { get; private set; }

        public void CloseWindow()
        {
            if (!IsClose)
                _view.Close();
        }

        public void DestroyWindow()
        {
            if(!IsClose)
                Release();            
        }

        private void OnViewClosed()
        {
            Release();
        }

        private void Release()
        {
            IsClose = true;
            
            _view.OnClosedEvent -= OnViewClosed;
            
            _cts.Cancel();
            _cts.Dispose();
            
            _release?.Invoke();
            
            OnClosedEvent?.Invoke();
        }
    }
}
