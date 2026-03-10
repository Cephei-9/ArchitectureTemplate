using System.Threading;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Presenter for the main menu window.
    /// </summary>
    public class MainMenuPresenter : IDefaultPresentationModel
    {
        public ArchitectureTemplate.AssetManagement.AssetKey ViewAssetKey => ArchitectureTemplate.AssetManagement.AssetKey.MainMenuWindow;
        public void Initialize(CancellationToken token)
        {
            
        }

        public void Dispose() { }
    }
}

