using System.Threading;
using Cysharp.Threading.Tasks;
using Initialization.InitializationPipeline;
using UnityEngine;

namespace ArchitectureTemplate.AssetManagement
{
    public class AddressablesInitializationStep : IInitializationPipelineStep
    {
        private const string StepNameConst = "AdressablesInitializationStep";
        private const float WeightStepConst = 0.2f;
        
        private AddressablesAssetService _service;

        public string Name => StepNameConst;
        public float Weight => WeightStepConst;

        public AddressablesInitializationStep(AddressablesAssetService service)
        {
            _service = service;
        }
        
        public async UniTask<bool> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _service.InitializeAsync(cancellationToken);
                
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}