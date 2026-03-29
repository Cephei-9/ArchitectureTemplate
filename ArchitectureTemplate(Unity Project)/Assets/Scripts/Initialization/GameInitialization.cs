using System.Collections.Generic;
using System.Threading;
using ArchitectureTemplate.AssetManagement;
using ArchitectureTemplate.MainMenu;
using ArchitectureTemplate.UI;
using Cysharp.Threading.Tasks;
using Initialization.InitializationPipeline;
using SceneLoading;
using UnityEngine;
using Zenject;

namespace ArchitectureTemplate.Initialization
{
    /// <summary>
    /// Startup entry point that runs the initialization pipeline.
    /// </summary>
    public class GameInitialization : IInitializable
    {
        private readonly InitializationPipelineService _pipelineService;
        private readonly SceneLoader _sceneLoader;
        private readonly WindowService _windowService;
        private readonly AddressablesInitializationStep _addressablesInitializationStep;
        private readonly ProjectAssetsInitializationStep _projectAssetsInitializationStep;

        public GameInitialization(InitializationPipelineService pipelineService, 
            AddressablesInitializationStep addressablesInitializationStep,
            ProjectAssetsInitializationStep projectAssetsInitializationStep,
            WindowService windowService,
            SceneLoader sceneLoader)
        {
            _addressablesInitializationStep = addressablesInitializationStep;
            _projectAssetsInitializationStep = projectAssetsInitializationStep;
            _pipelineService = pipelineService;
            _windowService = windowService;
            _sceneLoader = sceneLoader;
        }

        public void Initialize()
        {
            RunInitializationAsync().Forget();
        }

        private async UniTaskVoid RunInitializationAsync()
        {
            Debug.Log("[GameInitialization] Initialization started.");
            
            using CancellationTokenSource cts = new();

            bool isSuccess = await _pipelineService.RunAsync(ComposeInitializationSteps(), cts.Token);

            await _assetService.LoadAsync<Object>(AssetKey.InitializationScreen, cts.Token);
            _windowService.OpenWindow(UILayer.Screen, out InitializationScreenPresenter _);

            if (isSuccess)
            {
                Debug.Log("[GameInitialization] Game initialization completed successfully.");
                _windowService.OpenWindow(UILayer.Screen, out LoadingScreenPresenter _);
                LoadMainMenuAsync(cts.Token).Forget();

                return;
            }

            Debug.LogError("[GameInitialization] Game initialization failed.");
        }

        private List<IInitializationPipelineStep> ComposeInitializationSteps()
        {
            List<IInitializationPipelineStep> stepsList = new()
            {
                _addressablesInitializationStep,
                _projectAssetsInitializationStep
            };
            
            return stepsList;
        }

        private async UniTaskVoid LoadMainMenuAsync(CancellationToken token)
        {
            await _sceneLoader.LoadEmptySceneAsync(token);
            // Unload all initialization assets
            await _sceneLoader.LoadSceneAsync(SceneIds.MainMenu, token);
            
            MainMenuEntryPoint mainMenuEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
            mainMenuEntryPoint.EnterMainMenu();
        }
    }
}

