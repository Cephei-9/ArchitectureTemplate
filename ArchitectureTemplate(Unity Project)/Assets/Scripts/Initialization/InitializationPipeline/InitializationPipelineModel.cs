using System;
using System.Collections.Generic;
using UniRx;

namespace Initialization.InitializationPipeline
{
    /// <summary>
    /// Модель состояния инициализации, содержащая шаги и прогресс выполнения.
    /// </summary>
    public sealed class InitializationPipelineModel : IDisposable
    {
        public List<IInitializationPipelineStep> Steps = new(Array.Empty<IInitializationPipelineStep>());
        public ReactiveProperty<string> CurrentStepName = new(string.Empty);
        public ReactiveProperty<float> Progress = new(0f);

        public int I { get; private set; }

        public void Dispose()
        {
            CurrentStepName.Dispose();
            Progress.Dispose();
        }
    }
}
