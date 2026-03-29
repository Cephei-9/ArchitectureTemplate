using System.Collections.Generic;
using ArchitectureTemplate.AssetManagement;

namespace ArchitectureTemplate.Gameplay
{
    /// <summary>
    /// Loads assets required by the gameplay scene.
    /// </summary>
    public class GameplayAssetsLoader : AssetsLoaderBase
    {
        private readonly List<AssetKey> _assetKeysList = new() { AssetKey.GameplayScreen };

        public GameplayAssetsLoader(AssetService assetService) : base(assetService)
        {
        }

        protected override List<AssetKey> AssetKeys => _assetKeysList;
    }
}
