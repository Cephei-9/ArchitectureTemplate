namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Presenter for toast message popup.
    /// </summary>
    public class ToastPopupPresentationModel : IArgumentedPresentationModel<MessageArgs>
    {
        public ArchitectureTemplate.AssetManagement.AssetKey ViewAssetKey => ArchitectureTemplate.AssetManagement.AssetKey.ToastPopupWindow;

        public MessageArgs Args { get; private set; }

        public void InitializeArgument(MessageArgs argument, System.Threading.CancellationToken token)
        {
            Args = argument;
        }

        public void Dispose() { }
    }
}

