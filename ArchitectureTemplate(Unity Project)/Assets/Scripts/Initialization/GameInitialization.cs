using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace ArchitectureTemplate.Initialization
{
    /// <summary>
    /// Стартовый раннер игры.
    /// Получает сервис и единственный тестовый шаг из DI и запускает пайплайн.
    /// </summary>
    public sealed class GameInitialization : MonoBehaviour
    {
        private InitializationService _initializationService;
        private IInitializationStep _testStep;

        [Inject]
        public void Construct(InitializationService initializationService, IInitializationStep testStep)
        {
            _initializationService = initializationService;
            _testStep = testStep;
        }

        private void Start()
        {
            RunInitializationAsync().Forget();
        }

        private async UniTaskVoid RunInitializationAsync()
        {
            var steps = new List<IInitializationStep> { _testStep };
            _initializationService.SetSteps(steps);

            using var cts = new CancellationTokenSource();

            bool success = await _initializationService.RunAsync(cts.Token);

            if (success)
            {
                Debug.Log("[Initialization] Game initialization completed successfully.");
            }
            else
            {
                Debug.LogError("[Initialization] Game initialization failed.");
            }
        }
    }
}

