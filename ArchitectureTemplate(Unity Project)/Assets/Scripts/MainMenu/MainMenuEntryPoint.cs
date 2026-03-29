using System.Threading;
using ArchitectureTemplate.Initialization;
using ArchitectureTemplate.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace ArchitectureTemplate.MainMenu
{
    /// <summary>
    /// Entry point that opens the main menu screen.
    /// </summary>
    public class MainMenuEntryPoint : MonoBehaviour
    {
        private InitializationScreenService _initializationScreenService;
        private MainMenuAssetsLoader _mainMenuAssetsLoader;
        private WindowService _windowService;
        
        [Inject]
        public void Construct(WindowService windowService, MainMenuAssetsLoader mainMenuAssetsLoader,
            InitializationScreenService initializationScreenService)
        {
            _initializationScreenService = initializationScreenService;
            _windowService = windowService;
            _mainMenuAssetsLoader = mainMenuAssetsLoader;
        }
        
        public void EnterMainMenu()
        {
            EnterMainMenuAsync().Forget();
        }

        private async UniTaskVoid EnterMainMenuAsync()
        {
            using CancellationTokenSource cts = new();

            Debug.Log("[MainMenuEntryPoint] Main menu started.");

            await _mainMenuAssetsLoader.LoadAll(cts.Token);
            
            _initializationScreenService.Close();
            _windowService.CloseWindow<LoadingScreenPresenter>();
            
            _windowService.OpenWindow(UILayer.Screen, out MainMenuScreenPresenter _);
        }
    }
}
