namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Public service that provides access to the window system.
    /// </summary>
    public class WindowService
    {
        private readonly WindowManager _windowManager;

        public WindowService(WindowManager windowManager)
        {
            _windowManager = windowManager;
        }

        public bool IsOpen<TPresentationModel>() where TPresentationModel : IPresentationModel
        {
            return _windowManager.IsOpen<TPresentationModel>();
        }

        public WindowHandle OpenWindow<TPresentationModel>(UILayer layer, out TPresentationModel presentationModel)
            where TPresentationModel : class, IDefaultPresentationModel
        {
            return _windowManager.OpenWindow(layer, out presentationModel);
        }

        public WindowHandle OpenWindow<TPresentationModel, TArgs>(TArgs args, UILayer layer, out TPresentationModel presentationModel)
            where TPresentationModel : class, IArgumentedPresentationModel<TArgs>
        {
            return _windowManager.OpenWindow<TPresentationModel, TArgs>(args, layer, out presentationModel);
        }

        public WindowHandle CloseWindow<TPresentationModel>() where TPresentationModel : IPresentationModel
        {
            return _windowManager.CloseWindow<TPresentationModel>();
        }

        public void DestroyAll()
        {
            _windowManager.DestroyAll();
        }
    }
}
