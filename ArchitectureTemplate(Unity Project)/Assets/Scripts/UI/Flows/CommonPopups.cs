namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Shows error and toast popups via window service.
    /// </summary>
    public class CommonPopups : ICommonPopups
    {
        private readonly IWindowService _windows;

        public CommonPopups(IWindowService windows)
        {
            _windows = windows;
        }

        public void ShowError(string message)
        {
            _windows.Open(WindowId.ErrorPopup, new MessageArgs(message));
        }

        public void ShowToast(string message)
        {
            _windows.Open(WindowId.ToastPopup, new MessageArgs(message));
        }
    }
}
