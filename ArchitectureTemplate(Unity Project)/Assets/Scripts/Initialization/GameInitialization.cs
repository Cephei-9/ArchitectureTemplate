using System.Collections.Generic;
using System.Threading;
using ArchitectureTemplate.AssetManagement;
using ArchitectureTemplate.UI;
using Cysharp.Threading.Tasks;
using Initialization.InitializationPipeline;
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
        private readonly IInitializationPipelineStep _testStep;
        private readonly WindowService _windowService;
        private AddressablesInitializationStep _addressablesInitializationStep;
        private IAssetService _assetService;

        public GameInitialization(InitializationPipelineService pipelineService, 
            AddressablesInitializationStep addressablesInitializationStep,
            WindowService windowService, IAssetService assetService)
        {
            _addressablesInitializationStep = addressablesInitializationStep;
            _pipelineService = pipelineService;
            _windowService = windowService;
            _assetService = assetService;
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
                return;
            }

            Debug.LogError("[GameInitialization] Game initialization failed.");
        }

        private List<IInitializationPipelineStep> ComposeInitializationSteps()
        {
            List<IInitializationPipelineStep> stepsList = new() { _addressablesInitializationStep };
            
            return stepsList;
        }
    }
}

