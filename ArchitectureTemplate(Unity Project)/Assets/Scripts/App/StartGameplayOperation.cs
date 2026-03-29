using System.Threading;
using ArchitectureTemplate.Gameplay;
using ArchitectureTemplate.MainMenu;
using ArchitectureTemplate.UI;
using Cysharp.Threading.Tasks;
using SceneLoading;
using UnityEngine;

namespace Game.App
{
    /// <summary>
    /// Starts the gameplay flow by loading the gameplay scene.
    /// </summary>
    public class StartGameplayOperation
    {
        private readonly SceneLoader _sceneLoader;
        private MainMenuAssetsLoader _mainMenuAssetsLoader;
        private WindowService _windowService;

        public StartGameplayOperation(SceneLoader sceneLoader,
            MainMenuAssetsLoader mainMenuAssetsLoader,
            WindowService windowService)
        {
            _windowService = windowService;
            _mainMenuAssetsLoader = mainMenuAssetsLoader;
            _sceneLoader = sceneLoader;
        }

        public async UniTask<bool> StartGameplayAsync(CancellationToken token = default)
        {
            Debug.Log("[StartGameplayOperation] StartGameplayAsync started.");

            _windowService.DestroyAll();
            _windowService.OpenWindow(UILayer.Screen, out LoadingScreenPresenter _);

            await _sceneLoader.LoadEmptySceneAsync(token);

            _mainMenuAssetsLoader.ReleaseAll();

            await _sceneLoader.LoadSceneAsync(SceneIds.Gameplay, token);
            
            GameplayEntryPoint gameplayEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
            gameplayEntryPoint.EnterGameplay().Forget();

            Debug.Log("[StartGameplayOperation] Start gameplay succeeded.");
            return true;
        }
    }
}
