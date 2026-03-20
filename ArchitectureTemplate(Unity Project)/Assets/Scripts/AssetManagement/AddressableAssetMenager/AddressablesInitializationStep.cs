using System.Threading;
using Cysharp.Threading.Tasks;
using Initialization.InitializationPipeline;
using UnityEngine;

namespace ArchitectureTemplate.AssetManagement
{
    /// <summary>
    /// Initialization step that initializes the Addressables system.
    /// </summary>
    public class AddressablesInitializationStep : IInitializationPipelineStep
    {
        private const string StepNameConst = "AddressablesInitializationStep";
        private const float WeightStepConst = 0.2f;
        
        private readonly AddressablesAssetProvider _provider;

        public string Name => StepNameConst;
        public float Weight => WeightStepConst;

        public AddressablesInitializationStep(AddressablesAssetProvider provider)
        {
            _provider = provider;
        }
        
        public async UniTask<bool> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _provider.InitializeAsync(cancellationToken);
                
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
