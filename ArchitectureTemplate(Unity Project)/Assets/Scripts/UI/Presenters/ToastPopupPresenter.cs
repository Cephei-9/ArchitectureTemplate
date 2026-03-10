using System;
using System.Threading;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Presenter for toast message popup.
    /// </summary>
    public class ToastPopupPresentationModel : IArgumentedPresentationModel<MessageArgs>
    {
        public AssetId ViewAssetKey => AssetId.Ui_ToastPopup;

        public MessageArgs Args { get; private set; }

        public void Initialize()
        {
        }

        public void InitializeArgument(MessageArgs argument, CancellationToken ctsToken)
        {
            Args = argument;
        }

        public void Dispose()
        {
        }
    }
}

