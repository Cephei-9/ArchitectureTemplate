using System.Collections.Generic;
using System.Threading;
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

        public GameInitialization(InitializationPipelineService pipelineService, IInitializationPipelineStep testStep)
        {
            _pipelineService = pipelineService;
            _testStep = testStep;
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

            if (isSuccess)
            {
                Debug.Log("[GameInitialization] Game initialization completed successfully.");
                return;
            }

            Debug.LogError("[GameInitialization] Game initialization failed.");
        }

        private List<IInitializationPipelineStep> ComposeInitializationSteps()
        {
            List<IInitializationPipelineStep> stepsList = new() { _testStep };
            
            return stepsList;
        }
    }
}

