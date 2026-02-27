using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;

namespace Initialization.InitializationPipeline
{
    /// <summary>
    /// Сервис приложения для управления пайплайном инициализации и отслеживания его прогресса.
    /// </summary>
    public sealed class InitializationPipelineService : IDisposable
    {
        private readonly InitializationPipelineModel _model;
        private readonly InitializationPipelineRunner _runner;

        public InitializationPipelineService(InitializationPipelineModel model, InitializationPipelineRunner runner)
        {
            _model = model;
            _runner = runner;
        }

        public IReadOnlyReactiveProperty<string> CurrentStepName
        {
            get
            {
                return _model.CurrentStepName;
            }
        }

        public List<IInitializationPipelineStep> Steps => _model.Steps;

        public IReadOnlyReactiveProperty<float> Progress
        {
            get
            {
                return _model.Progress;
            }
        }

        public UniTask<bool> RunAsync(CancellationToken cancellationToken)
        {
            return _runner.RunAsync(_model.Steps, cancellationToken);
        }

        public UniTask<bool> RunAsync(List<IInitializationPipelineStep> steps, CancellationToken cancellationToken)
        {
            return _runner.RunAsync(steps, cancellationToken);
        }

        public void Dispose()
        {
            _model.Dispose();
        }
    }
}
