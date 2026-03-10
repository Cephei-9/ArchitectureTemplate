using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Manages open windows. Caches window handles and presenters by presenter type.
    /// </summary>
    public class WindowService
    {
        private readonly WindowFactory _factory;
        private readonly Dictionary<Type, (WindowHandle Handle, IPresentationModel Presenter)> _openMap = new();

        public WindowService(WindowFactory factory)
        {
            _factory = factory;
        }

        public bool IsOpen<TPresenter>() where TPresenter : IPresentationModel
        {
            return _openMap.ContainsKey(typeof(TPresenter));
        }

        /// <summary>
        /// Opens a window with a presenter that does not require arguments.
        /// Returns WindowHandle and presenter via out parameter.
        /// </summary>
        public WindowHandle OpenWindow<TPresenter>(out TPresenter presenter)
            where TPresenter : class, IDefaultPresentationModel
        {
            Type key = typeof(TPresenter);

            if (_openMap.TryGetValue(key, out (WindowHandle Handle, IPresentationModel Presenter) existing))
            {
                presenter = (TPresenter)existing.Presenter;
                return existing.Handle;
            }

            WindowHandle handle = _factory.Create(out presenter);
            
            _openMap[key] = (handle, presenter);
            handle.OnClosedEvent += () => _openMap.Remove(key);

            return handle;
        }

        /// <summary>
        /// Opens a window with an argumented presenter.
        /// Returns WindowHandle and presenter via out parameter.
        /// </summary>
        public WindowHandle OpenWindow<TPresenter, TArgs>(TArgs args, out TPresenter presenter)
            where TPresenter : class, IArgumentedPresentationModel<TArgs>
        {
            Type key = typeof(TPresenter);

            if (_openMap.TryGetValue(key, out (WindowHandle Handle, IPresentationModel Presenter) existing))
            {
                presenter = (TPresenter)existing.Presenter;
                return existing.Handle;
            }

            WindowHandle handle = _factory.Create(args, out presenter);
            _openMap[key] = (handle, presenter);
            handle.OnClosedEvent += () => _openMap.Remove(key);

            return handle;
        }

        public void CloseWindow<TPresenter>() where TPresenter : IPresentationModel
        {
            Type key = typeof(TPresenter);

            if (_openMap.TryGetValue(key, out (WindowHandle Handle, IPresentationModel Presenter) existing)) 
                existing.Handle.CloseWindow();
        }
    }
}
