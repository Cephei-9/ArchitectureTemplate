using System;
using System.Threading;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Presenter for error message popup.
    /// </summary>
    public class ErrorPopupPresentationModel : IArgumentedPresentationModel<MessageArgs>
    {
        public AssetId ViewAssetKey => AssetId.Ui_ErrorPopup;

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

