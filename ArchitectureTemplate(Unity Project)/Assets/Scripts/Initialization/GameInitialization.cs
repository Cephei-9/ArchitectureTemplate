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
        private readonly AssetService _assetService;

        public GameInitialization(InitializationPipelineService pipelineService, 
            AddressablesInitializationStep addressablesInitializationStep,
            WindowService windowService, AssetService assetService, SceneLoader sceneLoader)
        {
            _addressablesInitializationStep = addressablesInitializationStep;
            _pipelineService = pipelineService;
            _windowService = windowService;
            _assetService = assetService;
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
            
            // В первую очередь нужно выполнить инициализацию адрессаблов, следом включить шторку, и уже потом выполнять
            // всю прочую инициализацию. Это важно, потмоу что перед общей инициализацией, нам нужно открыть шторку,
            // а для этого нужны адрессаблы, так что они первые, и без вопросов. Ну или можно было бы просто держать
            // шторку обособленно от остального UI и грузить ее сразу в сцену. Но пока это излишне
            
            bool isSuccess = await _pipelineService.RunAsync(ComposeInitializationSteps(), cts.Token);

            await _assetService.LoadAsync<Object>(AssetKey.InitializationScreen, cts.Token);
            _windowService.OpenWindow(UILayer.Screen, out InitializationScreenPresenter _);

            if (isSuccess)
            {
                Debug.Log("[GameInitialization] Game initialization completed successfully.");
                LoadMainMenuAsync(cts.Token).Forget();

                return;
            }

            Debug.LogError("[GameInitialization] Game initialization failed.");
        }

        private List<IInitializationPipelineStep> ComposeInitializationSteps()
        {
            List<IInitializationPipelineStep> stepsList = new() { _addressablesInitializationStep };
            
            return stepsList;
        }

        private async UniTaskVoid LoadMainMenuAsync(CancellationToken token)
        {
            await _sceneLoader.LoadEmptySceneAsync();
            // Unload all initialization assets
            await _sceneLoader.LoadSceneAsync(SceneIds.MainMenu, token);
            
            MainMenuEntryPoint mainMenuEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
            mainMenuEntryPoint.EnterMainMenu();
        }
    }
}

