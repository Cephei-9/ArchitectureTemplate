using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace ArchitectureTemplate.UI
{
    public class WindowService
    {
        private readonly WindowFactory _factory;
        private readonly Dictionary<Type, (WindowHandle Handle, IPresentationModel PresentationModel)> _openMap = new();

        public WindowService(WindowFactory factory)
        {
            _factory = factory;
        }

        public bool IsOpen<TPresenter>() where TPresenter : IPresentationModel
        {
            return _openMap.ContainsKey(typeof(TPresenter));
        }

        public WindowHandle OpenWindow<TPresentationModel>(out TPresentationModel presentationModel)
            where TPresentationModel : class, IDefaultPresentationModel
        {
            Type key = typeof(TPresentationModel);

            if (_openMap.TryGetValue(key, out (WindowHandle Handle, IPresentationModel PresentationModel) existing))
            {
                presentationModel = (TPresentationModel)existing.PresentationModel;
                return existing.Handle;
            }

            WindowHandle handle = _factory.Create(out presentationModel);
            
            _openMap[key] = (handle, presentationModel);
            handle.OnClosedEvent += () => _openMap.Remove(key);

            return handle;
        }

        public WindowHandle OpenWindow<TPresentationModel, TArgs>(TArgs args, out TPresentationModel presentationModel)
            where TPresentationModel : class, IArgumentedPresentationModel<TArgs>
        {
            Type key = typeof(TPresentationModel);

            if (_openMap.TryGetValue(key, out (WindowHandle Handle, IPresentationModel PresentationModel) existing))
            {
                presentationModel = (TPresentationModel)existing.PresentationModel;
                return existing.Handle;
            }

            WindowHandle handle = _factory.Create(args, out presentationModel);
            _openMap[key] = (handle, presentationModel);
            handle.OnClosedEvent += () => _openMap.Remove(key);

            return handle;
        }

        public void CloseWindow<TPresentationModel>() where TPresentationModel : IPresentationModel
        {
            Type key = typeof(TPresentationModel);

            if (_openMap.TryGetValue(key, out (WindowHandle Handle, IPresentationModel Presenter) existing)) 
                existing.Handle.CloseWindow();
        }
    }
}
