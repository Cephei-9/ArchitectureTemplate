using System.Threading;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Presenter for the settings popup window.
    /// </summary>
    public class SettingsPopupPresenter : IDefaultPresentationModel
    {
        public ArchitectureTemplate.AssetManagement.AssetKey ViewAssetKey => ArchitectureTemplate.AssetManagement.AssetKey.SettingsWindow;
        public void Initialize(CancellationToken token)
        {
            
        }

        public void Dispose() { }
    }
}

