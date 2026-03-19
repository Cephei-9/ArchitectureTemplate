using System.Threading;
using ArchitectureTemplate.AssetManagement;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Presentation model for the initialization screen.
    /// </summary>
    public class InitializationScreenPresenter : IDefaultPresentationModel
    {
        public AssetKey ViewAssetKey => AssetKey.InitializationScreen;

        public void Initialize(CancellationToken token)
        {
        }

        public void Dispose() { }
    }
}

