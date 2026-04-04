using System.Threading;
using ArchitectureTemplate.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace ArchitectureTemplate.Gameplay
{
    /// <summary>
    /// Entry point that opens the gameplay screen.
    /// </summary>
    public class GameplayEntryPoint : MonoBehaviour
    {
        private GameplayAssetsLoader _gameplayAssetsLoader;
        private WindowService _windowService;

        [Inject]
        public void Construct(WindowService windowService, GameplayAssetsLoader gameplayAssetsLoader)
        {
            _windowService = windowService;
            _gameplayAssetsLoader = gameplayAssetsLoader;
        }

        public async UniTaskVoid EnterGameplay()
        {
            Debug.Log("[GameplayEntryPoint] Gameplay started.");

            using CancellationTokenSource cts = new();
            
            await _gameplayAssetsLoader.LoadAll(cts.Token);
            
            _windowService.CloseWindow<LoadingScreenPresenter>();
            _windowService.OpenWindow(UILayer.Screen, out GameplayScreenPresenter _);
        }
    }
}
