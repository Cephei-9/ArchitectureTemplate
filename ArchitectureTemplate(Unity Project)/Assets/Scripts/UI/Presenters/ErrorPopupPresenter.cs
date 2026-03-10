namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Presenter for error message popup.
    /// </summary>
    public class ErrorPopupPresentationModel : IArgumentedPresentationModel<MessageArgs>
    {
        public ArchitectureTemplate.AssetManagement.AssetKey ViewAssetKey => ArchitectureTemplate.AssetManagement.AssetKey.ErrorPopupWindow;

        public MessageArgs Args { get; private set; }

        public void InitializeArgument(MessageArgs argument, System.Threading.CancellationToken token)
        {
            Args = argument;
        }

        public void Dispose() { }
    }
}

