using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Initialization.InitializationPipeline
{
    /// <summary>
    /// Пайплайн, который последовательно выполняет шаги инициализации и обновляет модель прогресса.
    /// </summary>
    public sealed class InitializationPipelineRunner
    {
        private readonly InitializationPipelineModel _model;

        public InitializationPipelineRunner(InitializationPipelineModel model)
        {
            _model = model;
        }

        public async UniTask<bool> RunAsync(CancellationToken cancellationToken)
        {
            if (_model.Steps.Count == 0)
            {
                _model.CurrentStepName.Value = string.Empty;
                _model.Progress.Value = 1f;
                return true;
            }

            float totalWeight = _model.Steps.Sum((IInitializationPipelineStep step) => step == null ? 0f : Mathf.Max(0f, step.Weight));
            float completedWeight = 0f;

            _model.Progress.Value = 0f;

            foreach (IInitializationPipelineStep step in _model.Steps)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return false;
                }

                if (step == null)
                {
                    return false;
                }

                if (step.Weight <= 0f)
                {
                    return false;
                }

                _model.CurrentStepName.Value = step.Name ?? string.Empty;
                _model.Progress.Value = totalWeight <= 0f ? 0f : Mathf.Clamp01(completedWeight / totalWeight);

                try
                {
                    await step.ExecuteAsync(cancellationToken);
                }
                catch
                {
                    return false;
                }

                completedWeight += step.Weight;
                _model.Progress.Value = totalWeight <= 0f ? 1f : Mathf.Clamp01(completedWeight / totalWeight);
            }

            _model.CurrentStepName.Value = string.Empty;
            _model.Progress.Value = 1f;
            return true;
        }
    }
}
