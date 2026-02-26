using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;

namespace ArchitectureTemplate.Initialization
{
    public sealed class InitializationService : IDisposable
    {
        private readonly InitializationModel _model;
        private readonly PipelineRunner _pipelineRunner;

        public InitializationService()
        {
            _model = new InitializationModel();
            _pipelineRunner = new PipelineRunner(_model);
        }

        public IReadOnlyReactiveProperty<string> CurrentStepName => _model.CurrentStepName;
        public IReadOnlyReactiveProperty<float> Progress => _model.Progress;

        public void SetSteps(IReadOnlyList<IInitializationStep> steps)
        {
            _model.Steps = steps ?? Array.Empty<IInitializationStep>();
        }

        public UniTask<bool> RunAsync(CancellationToken cancellationToken)
        {
            return _pipelineRunner.RunAsync(cancellationToken);
        }

        public void Dispose()
        {
            _model.Dispose();
        }
    }
}
