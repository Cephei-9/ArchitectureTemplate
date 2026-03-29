using System.Collections.Generic;
using ArchitectureTemplate.AssetManagement;

namespace ArchitectureTemplate.MainMenu
{
    /// <summary>
    /// Loads assets required by the main menu scene.
    /// </summary>
    public class MainMenuAssetsLoader : AssetsLoaderBase
    {
        private readonly List<AssetKey> _assetKeysList = new() { AssetKey.MainMenuScreen };

        public MainMenuAssetsLoader(AssetService assetService) : base(assetService)
        {
        }

        protected override List<AssetKey> AssetKeys => _assetKeysList;
    }
}
