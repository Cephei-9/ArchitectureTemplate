namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Shows error and toast popups via window service.
    /// </summary>
    public class CommonPopups
    {
        private readonly WindowService _windows;

        public CommonPopups(WindowService windows)
        {
            _windows = windows;
        }

        public void ShowError(string message)
        {
            _windows.OpenWindow<ErrorPopupPresentationModel, MessageArgs>(new MessageArgs(message), UILayer.Popups, out _);
        }

        public void ShowToast(string message)
        {
            _windows.OpenWindow<ToastPopupPresentationModel, MessageArgs>(new MessageArgs(message), UILayer.Popups, out _);
        }
    }
}
