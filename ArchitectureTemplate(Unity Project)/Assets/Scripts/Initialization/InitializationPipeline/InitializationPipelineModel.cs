using System;
using System.Collections.Generic;
using UniRx;

namespace Initialization.InitializationPipeline
{
    /// <summary>
    /// Mutable state model for initialization pipeline execution.
    /// </summary>
    public class InitializationPipelineModel : IDisposable
    {
        public List<IInitializationPipelineStep> StepsList = new(Array.Empty<IInitializationPipelineStep>());
        public ReactiveProperty<string> CurrentStepName = new(string.Empty);
        public ReactiveProperty<float> Progress = new(0f);

        public void Dispose()
        {
            CurrentStepName.Dispose();
            Progress.Dispose();
        }
    }
}

