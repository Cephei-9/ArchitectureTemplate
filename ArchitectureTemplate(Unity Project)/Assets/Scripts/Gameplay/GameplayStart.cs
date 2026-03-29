using System.Threading;
using ArchitectureTemplate.AssetManagement;
using ArchitectureTemplate.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace ArchitectureTemplate.Gameplay
{
    /// <summary>
    /// Entry point that opens the gameplay screen.
    /// </summary>
    public class GameplayStart : IInitializable
    {
        private readonly AssetService _assetService;
        private readonly GameplayAssetsLoader _gameplayAssetsLoader;
        private readonly WindowService _windowService;

        public GameplayStart(WindowService windowService, GameplayAssetsLoader gameplayAssetsLoader, AssetService assetService)
        {
            _windowService = windowService;
            _gameplayAssetsLoader = gameplayAssetsLoader;
            _assetService = assetService;
        }

        public void Initialize()
        {
            EnterGameplay().Forget();
        }

        private async UniTaskVoid EnterGameplay()
        {
            Debug.Log("[GameplayStart] Gameplay started.");

            using CancellationTokenSource cts = new();
            await _gameplayAssetsLoader.LoadAll(cts.Token);
            
            _windowService.OpenWindow(UILayer.Screen, out GameplayScreenPresenter _);
        }
    }
}
