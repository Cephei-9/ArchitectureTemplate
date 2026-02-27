using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Initialization.InitializationPipeline;
using Zenject;

namespace ArchitectureTemplate.Initialization
{
    /// <summary>
    /// Стартовый раннер игры.
    /// Получает сервис и единственный тестовый шаг из DI и запускает пайплайн.
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
            List<IInitializationPipelineStep> steps = new List<IInitializationPipelineStep> { _testStep };
            _pipelineService.SetSteps(steps);

            using CancellationTokenSource cts = new CancellationTokenSource();

            bool success = await _pipelineService.RunAsync(cts.Token);

            if (success)
            {
                UnityEngine.Debug.Log("[Initialization] Game initialization completed successfully.");
            }
            else
            {
                UnityEngine.Debug.LogError("[Initialization] Game initialization failed.");
            }
        }
    }
}

