using System;
using System.Collections.Generic;
using UniRx;

namespace ArchitectureTemplate.Initialization
{
    public sealed class InitializationModel : IDisposable
    {
        public IReadOnlyList<IInitializationStep> Steps { get; set; } = Array.Empty<IInitializationStep>();
        public ReactiveProperty<string> CurrentStepName { get; } = new(string.Empty);
        public ReactiveProperty<float> Progress { get; } = new(0f);

        public void Dispose()
        {
            CurrentStepName.Dispose();
            Progress.Dispose();
        }
    }
}
