using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace ArchitectureTemplate.AssetManagement
{
    /// <summary>
    /// Example loader that preloads and releases main menu assets.
    /// </summary>
    public class MainMenuAssetsLoader
    {
        private static readonly AssetKey[] Keys = {
            AssetKey.InitializationScreen,
            AssetKey.LoadingCurtain,
            AssetKey.SettingsWindow
        };

        private readonly IAssetService _assets;

        public MainMenuAssetsLoader(IAssetService assets)
        {
            _assets = assets;
        }

        public UniTask LoadAsync(CancellationToken cancellationToken = default)
        {
            return _assets.LoadManyAsync(Keys, cancellationToken);
        }

        public void Release()
        {
            _assets.ReleaseMany(Keys);
        }
    }
}
