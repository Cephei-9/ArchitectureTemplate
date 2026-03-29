using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace ArchitectureTemplate.AssetManagement
{
    /// <summary>
    /// Base class for scoped asset loaders. Derive from it for each asset scope, implement
    /// </summary>
    public abstract class AssetsLoaderBase
    {
        protected AssetService AssetService;
        
        protected abstract List<AssetKey> AssetKeys { get; }

        protected AssetsLoaderBase(AssetService assetService)
        {
            AssetService = assetService;
        }

        public virtual async UniTask LoadAll(CancellationToken token = default)
        {
            await AssetService.LoadManyAsync(AssetKeys, token);
        } 
        
        public virtual void ReleaseAll()
        {
            AssetService.ReleaseMany(AssetKeys);
        } 
    }
}
