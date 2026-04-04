using System.Threading;
using ArchitectureTemplate.AssetManagement;
using ArchitectureTemplate.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ArchitectureTemplate.Initialization
{
    /// <summary>
    /// Application service that manages initialization screen lifecycle.
    /// </summary>
    public class InitializationScreenService
    {
        private readonly AssetService _assetService;
        private readonly WindowService _windowService;

        private bool _isReleased = true;

        public InitializationScreenService(AssetService assetService, WindowService windowService)
        {
            _assetService = assetService;
            _windowService = windowService;
        }

        public async UniTask OpenAsync(CancellationToken cancellationToken = default)
        {
            if (_isReleased)
            {
                await _assetService.LoadAsync<GameObject>(AssetKey.InitializationScreen, cancellationToken);
                _isReleased = false;
            }

            _windowService.OpenWindow(UILayer.Screen, out InitializationScreenPresenter _);
        }

        public void Close()
        {
            if (_isReleased)
                return;

            _windowService.CloseWindow<InitializationScreenPresenter>();
            _assetService.Release(AssetKey.InitializationScreen);
            _isReleased = true;
        }
    }
}
