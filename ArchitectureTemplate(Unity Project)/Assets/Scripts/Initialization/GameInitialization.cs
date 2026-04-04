using System.Collections.Generic;
using System.Threading;
using ArchitectureTemplate.AssetManagement;
using ArchitectureTemplate.MainMenu;
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
        private readonly AddressablesInitializationStep _addressablesInitializationStep;
        private readonly ProjectAssetsInitializationStep _projectAssetsInitializationStep;
        private InitializationScreenService _initializationScreenService;

        public GameInitialization(InitializationPipelineService pipelineService, 
            AddressablesInitializationStep addressablesInitializationStep,
            ProjectAssetsInitializationStep projectAssetsInitializationStep,
            SceneLoader sceneLoader,
            InitializationScreenService initializationScreenService)
        {
            _initializationScreenService = initializationScreenService;
            _addressablesInitializationStep = addressablesInitializationStep;
            _projectAssetsInitializationStep = projectAssetsInitializationStep;
            _pipelineService = pipelineService;
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

            await _addressablesInitializationStep.ExecuteAsync(cts.Token);
            await _initializationScreenService.OpenAsync(cts.Token);
            
            bool isSuccess = await _pipelineService.RunAsync(ComposeInitializationSteps(), cts.Token);

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
            List<IInitializationPipelineStep> stepsList = new()
            {
                _projectAssetsInitializationStep
            };
            
            return stepsList;
        }

        private async UniTaskVoid LoadMainMenuAsync(CancellationToken token)
        {
            await _sceneLoader.LoadEmptySceneAsync(token);
            await _sceneLoader.LoadSceneAsync(SceneIds.MainMenu, token);
            
            MainMenuEntryPoint mainMenuEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
            mainMenuEntryPoint.EnterMainMenu();
        }
    }
}

