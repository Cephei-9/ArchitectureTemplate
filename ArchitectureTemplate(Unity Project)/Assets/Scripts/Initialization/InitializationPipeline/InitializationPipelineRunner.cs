using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Initialization.InitializationPipeline
{
    /// <summary>
    /// Runs initialization steps sequentially and updates execution progress.
    /// </summary>
    public class InitializationPipelineRunner
    {
        private readonly InitializationPipelineModel _model;

        public InitializationPipelineRunner(InitializationPipelineModel model)
        {
            _model = model;
        }

        public async UniTask<bool> RunAsync(List<IInitializationPipelineStep> stepsList, CancellationToken cancellationToken = default)
        {
            if (TryCompleteEmptyPipeline(stepsList))
                return true;

            float totalWeight = stepsList.Sum(step => Mathf.Max(0f, step.Weight));
            float completedWeight = 0f;
            
            _model.Progress.Value = 0f;

            foreach (IInitializationPipelineStep step in stepsList)
            {
                if (!await ExecuteStepAsync(step, totalWeight, completedWeight, cancellationToken))
                    return false;

                completedWeight += step.Weight;
            }

            _model.CurrentStepName.Value = string.Empty;
            _model.Progress.Value = 1f;
            Debug.Log("[InitializationPipelineRunner] Pipeline completed successfully.");
            
            return true;
        }

        private bool TryCompleteEmptyPipeline(List<IInitializationPipelineStep> stepsList)
        {
            if (stepsList.Count > 0)
                return false;

            _model.CurrentStepName.Value = string.Empty;
            _model.Progress.Value = 1f;
            Debug.Log("[InitializationPipelineRunner] Pipeline has no steps. Completed immediately.");
            return true;
        }

        private async UniTask<bool> ExecuteStepAsync(
            IInitializationPipelineStep step,
            float totalWeight,
            float completedWeight,
            CancellationToken cancellationToken)
        {
            _model.CurrentStepName.Value = step.Name;
            _model.Progress.Value = Mathf.Clamp01(completedWeight / totalWeight);
            
            Debug.Log($"[InitializationPipelineRunner] Step started: {step.Name}.");

            try
            {
                bool isSuccess = await step.ExecuteAsync(cancellationToken);

                if (!isSuccess)
                {
                    Debug.LogError($"[InitializationPipelineRunner] Step failed: {step.Name}.");
                    return false;
                }
            }
            catch (System.Exception exception)
            {
                Debug.LogError($"[InitializationPipelineRunner] Step failed: {step.Name}. Exception: {exception.Message}");
                return false;
            }

            Debug.Log($"[InitializationPipelineRunner] Step completed: {step.Name}.");
            return true;
        }
    }
}
