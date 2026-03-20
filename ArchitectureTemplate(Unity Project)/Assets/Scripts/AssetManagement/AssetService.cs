using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ArchitectureTemplate.AssetManagement
{
    /// <summary>
    /// Public service that provides access to asset loading and releasing.
    /// </summary>
    public class AssetService
    {
        private readonly IAssetProvider _assetProvider;

        public AssetService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public UniTask<T> LoadAsync<T>(AssetKey key, CancellationToken cancellationToken = default) where T : Object
        {
            return _assetProvider.LoadAsync<T>(key, cancellationToken);
        }

        public UniTask LoadManyAsync(IEnumerable<AssetKey> keys, CancellationToken cancellationToken = default)
        {
            return _assetProvider.LoadManyAsync(keys, cancellationToken);
        }

        public T GetAsset<T>(AssetKey key) where T : Object
        {
            return _assetProvider.GetAsset<T>(key);
        }

        public T InstantiateAsset<T>(AssetKey key) where T : Object
        {
            return _assetProvider.InstantiateAsset<T>(key);
        }

        public void Release(AssetKey key)
        {
            _assetProvider.Release(key);
        }

        public void ReleaseMany(IEnumerable<AssetKey> keys)
        {
            _assetProvider.ReleaseMany(keys);
        }

        public void ReleaseAll()
        {
            _assetProvider.ReleaseAll();
        }
    }
}
