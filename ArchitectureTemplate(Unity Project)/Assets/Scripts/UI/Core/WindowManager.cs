using System;
using System.Collections.Generic;
using System.Linq;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Manager that tracks open windows and delegates creation to WindowFactory.
    /// </summary>
    public class WindowManager
    {
        private readonly WindowFactory _factory;
        
        private readonly Dictionary<Type, (WindowHandle Handle, IPresentationModel PresentationModel)> _openMap = new();

        public WindowManager(WindowFactory factory)
        {
            _factory = factory;
        }

        public bool IsOpen<TPresenter>() where TPresenter : IPresentationModel
        {
            return _openMap.ContainsKey(typeof(TPresenter));
        }

        public WindowHandle OpenWindow<TPresentationModel>(UILayer layer, out TPresentationModel presentationModel)
            where TPresentationModel : class, IDefaultPresentationModel
        {
            Type key = typeof(TPresentationModel);

            if (_openMap.TryGetValue(key, out (WindowHandle Handle, IPresentationModel PresentationModel) existing))
            {
                presentationModel = (TPresentationModel)existing.PresentationModel;
                return existing.Handle;
            }

            WindowHandle handle = _factory.Create(layer, out presentationModel);
            
            _openMap[key] = (handle, presentationModel);
            handle.OnClosedEvent += () => _openMap.Remove(key);

            return handle;
        }

        public WindowHandle OpenWindow<TPresentationModel, TArgs>(TArgs args, UILayer layer, out TPresentationModel presentationModel)
            where TPresentationModel : class, IArgumentedPresentationModel<TArgs>
        {
            Type key = typeof(TPresentationModel);

            if (_openMap.TryGetValue(key, out (WindowHandle Handle, IPresentationModel PresentationModel) existing))
            {
                presentationModel = (TPresentationModel)existing.PresentationModel;
                return existing.Handle;
            }

            WindowHandle handle = _factory.Create(args, layer, out presentationModel);
            _openMap[key] = (handle, presentationModel);
            handle.OnClosedEvent += () => _openMap.Remove(key);

            return handle;
        }

        public WindowHandle CloseWindow<TPresentationModel>() where TPresentationModel : IPresentationModel
        {
            Type key = typeof(TPresentationModel);

            if (_openMap.TryGetValue(key, out (WindowHandle Handle, IPresentationModel Presenter) existing))
            {
                existing.Handle.CloseWindow();
                return existing.Handle;
            }

            return null;
        }

        public void DestroyAll()
        {
            List<WindowHandle> handles = new(_openMap.Select(entry => entry.Value.Handle));

            foreach (WindowHandle handle in handles)
            {
                handle.DestroyWindow();
            }
        }
    }
}
