using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Initialization.InitializationPipeline;
using UnityEngine;

namespace ArchitectureTemplate.Initialization.Testing
{
    /// <summary>
    /// Test step that validates basic initialization pipeline execution.
    /// </summary>
    public class TestPipelineStep : IInitializationPipelineStep
    {
        public string Name => "TestPipelineStep";
        public float Weight => 1f;

        public async UniTask<bool> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            TimeSpan delay = TimeSpan.FromMilliseconds(250);
            await UniTask.Delay(delay, cancellationToken: cancellationToken);
            return true;
        }
    }
}
