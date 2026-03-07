using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ArchitectureTemplate.AssetManagement
{
    /// <summary>
    /// Service for loading and releasing assets via Addressables.
    /// </summary>
    public interface IAssetService
    {
        UniTask<T> LoadAsync<T>(AssetKey key, CancellationToken cancellationToken = default) where T : Object;

        UniTask LoadManyAsync(IEnumerable<AssetKey> keys, CancellationToken cancellationToken = default);

        T GetAsset<T>(AssetKey key) where T : Object;

        T InstantiateAsset<T>(AssetKey key) where T : Object; 

        void Release(AssetKey key);

        void ReleaseMany(IEnumerable<AssetKey> keys);

        void ReleaseAll();
    }
}
