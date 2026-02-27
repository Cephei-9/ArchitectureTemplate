using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Initialization.InitializationPipeline;

namespace ArchitectureTemplate.Initialization.Testing
{
    /// <summary>
    /// Тестовый шаг пайплайна для проверки базовой работы инициализации.
    /// </summary>
    public sealed class TestPipelineStep : IInitializationPipelineStep
    {
        public string Name
        {
            get
            {
                return "TestPipelineStep";
            }
        }

        public float Weight
        {
            get
            {
                return 1f;
            }
        }

        public async UniTask ExecuteAsync(CancellationToken cancellationToken)
        {
            TimeSpan delay = TimeSpan.FromMilliseconds(250);
            await UniTask.Delay(delay, cancellationToken: cancellationToken);
        }
    }
}

