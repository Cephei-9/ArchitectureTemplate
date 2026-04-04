using System.Threading;
using ArchitectureTemplate.AssetManagement;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Presentation model for the loading screen.
    /// </summary>
    public class LoadingScreenPresenter : IDefaultPresentationModel
    {
        public AssetKey ViewAssetKey => AssetKey.LoadingScreen;

        public void Initialize(CancellationToken token) { }

        public void Dispose() { }
    }
}
