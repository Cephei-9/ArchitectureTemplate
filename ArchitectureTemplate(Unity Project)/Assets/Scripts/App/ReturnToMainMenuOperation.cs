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
    /// Returns to the main menu flow by loading the main menu scene.
    /// </summary>
    public class ReturnToMainMenuOperation
    {
        private readonly SceneLoader _sceneLoader;
        private GameplayAssetsLoader _gameplayAssetsLoader;
        private WindowService _windowService;

        public ReturnToMainMenuOperation(SceneLoader sceneLoader,
            GameplayAssetsLoader gameplayAssetsLoader,
            WindowService windowService)
        {
            _windowService = windowService;
            _gameplayAssetsLoader = gameplayAssetsLoader;
            _sceneLoader = sceneLoader;
        }

        public async UniTask<bool> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            Debug.Log("[ReturnToMainMenuOperation] ExecuteAsync started.");

            await _sceneLoader.LoadEmptySceneAsync(cancellationToken);
            
            _windowService.DestroyAll();
            _gameplayAssetsLoader.ReleaseAll();

            await _sceneLoader.LoadSceneAsync(SceneIds.MainMenu, cancellationToken);

            MainMenuEntryPoint mainMenuEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
            mainMenuEntryPoint.EnterMainMenu();

            Debug.Log("[ReturnToMainMenuOperation] Return to main menu succeeded.");
            return true;
        }
    }
}
