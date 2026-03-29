using System.Collections.Generic;
using System.Threading;
using ArchitectureTemplate.AssetManagement;
using Cysharp.Threading.Tasks;
using Initialization.InitializationPipeline;

namespace ArchitectureTemplate.Initialization
{
    /// <summary>
    /// Initialization step that preloads project UI assets required for scene transitions.
    /// </summary>
    public class ProjectAssetsInitializationStep : IInitializationPipelineStep
    {
        private const string StepNameConst = "ProjectAssetsInitializationStep";
        private const float WeightStepConst = 0.8f;

        private readonly AssetService _assetService;
        private readonly List<AssetKey> _assetKeysList = new()
        {
            AssetKey.LoadingScreen
        };

        public string Name => StepNameConst;
        public float Weight => WeightStepConst;

        public ProjectAssetsInitializationStep(AssetService assetService)
        {
            _assetService = assetService;
        }

        public async UniTask<bool> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _assetService.LoadManyAsync(_assetKeysList, cancellationToken);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
