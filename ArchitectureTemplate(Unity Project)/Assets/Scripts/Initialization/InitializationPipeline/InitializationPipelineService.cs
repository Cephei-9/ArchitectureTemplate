using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Initialization.InitializationPipeline
{
    /// <summary>
    /// Application service that exposes initialization pipeline state and execution API.
    /// </summary>
    public class InitializationPipelineService : IDisposable
    {
        private readonly InitializationPipelineModel _model;
        private readonly InitializationPipelineRunner _runner;

        public InitializationPipelineService(InitializationPipelineModel model, InitializationPipelineRunner runner)
        {
            _model = model;
            _runner = runner;
        }

        public IReadOnlyReactiveProperty<string> CurrentStepName => _model.CurrentStepName;
        public List<IInitializationPipelineStep> StepsList => _model.StepsList;
        public IReadOnlyReactiveProperty<float> Progress => _model.Progress;

        public UniTask<bool> RunAsync(CancellationToken cancellationToken = default)
        {
            return _runner.RunAsync(_model.StepsList, cancellationToken);
        }

        public UniTask<bool> RunAsync(List<IInitializationPipelineStep> stepsList, CancellationToken cancellationToken = default)
        {
            return _runner.RunAsync(stepsList, cancellationToken);
        }

        public void Dispose()
        {
            _model.Dispose();
        }
    }
}
