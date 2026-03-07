using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace ArchitectureTemplate.AssetManagement
{
    /// <summary>
    /// Addressables-backed asset service. Works with single assets only; handles are kept inside the service
    /// with ref-counting per key so multiple subsystems can load the same asset safely.
    /// Uses key.ToString() as the Addressables address.
    /// </summary>
    public class AddressablesAssetService : IAssetService
    {
        private AsyncOperationHandle _initHandle;
        
        private bool _initialized;

        private readonly Dictionary<AssetKey, AssetEntry> _entriesMap = new();

        public async UniTask InitializeAsync(CancellationToken cancellationToken = default)
        {
            if (_initialized)
                return;

            _initHandle = Addressables.InitializeAsync();
            
            await _initHandle.Task;

            if (_initHandle.Status != AsyncOperationStatus.Succeeded)
                throw new InvalidOperationException("Addressables.InitializeAsync failed.");

            _initialized = true;
            Debug.Log("[AddressablesAssetService] Initialized.");
        }

        public async UniTask<T> LoadAsync<T>(AssetKey key, CancellationToken cancellationToken = default) where T : Object
        {
            if (_entriesMap.TryGetValue(key, out AssetEntry existingAsset))
            {
                existingAsset.ReferenceCount++;

                if (existingAsset.Handle.IsValid() && existingAsset.Handle.Status == AsyncOperationStatus.Succeeded)
                    return (T)existingAsset.Handle.Result;

                await existingAsset.Handle.Task;
                return (T)existingAsset.Handle.Result;
            }

            AsyncOperationHandle handle = Addressables.LoadAssetAsync<T>(key.ToString());

            AssetEntry assetEntry = new(handle, 1);
            _entriesMap.Add(key, assetEntry);

            try
            {
                await handle.Task;
            }
            catch (OperationCanceledException)
            {
                SafeRelease(key, handle);
                throw;
            }

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                SafeRelease(key, handle);
                throw new InvalidOperationException($"Load failed for asset key: {key}.");
            }

            return (T)handle.Result;
        }

        public async UniTask LoadManyAsync(IEnumerable<AssetKey> keys, CancellationToken cancellationToken = default)
        {
            IEnumerable<AssetKey> distinctKeys = keys.Distinct();
            
            foreach (AssetKey k in distinctKeys)
            {
                await LoadAsync<Object>(k, cancellationToken);
            }
        }

        public T GetAsset<T>(AssetKey key) where T : Object
        {
            AssetEntry assetEntry = _entriesMap[key];
            
            return (T)assetEntry.Handle.Result;
        }

        public T InstantiateAsset<T>(AssetKey key) where T : Object
        {
            T asset = GetAsset<T>(key);
            T instance = Object.Instantiate(asset);

            return instance;
        }

        public void Release(AssetKey key)
        {
            if (!_entriesMap.TryGetValue(key, out AssetEntry entry))
                return;

            entry.ReferenceCount--;

            if (entry.ReferenceCount <= 0) 
                SafeRelease(key, entry.Handle);
        }

        public void ReleaseMany(IEnumerable<AssetKey> keys)
        {
            IEnumerable<AssetKey> distinctKeys = keys.Distinct();
            
            foreach (AssetKey key in distinctKeys)
            {
                Release(key);
            }
        }

        public void ReleaseAll()
        {
            foreach (KeyValuePair<AssetKey, AssetEntry> kv in _entriesMap)
            {
                ReleaseHandle(kv.Value.Handle);
            }

            _entriesMap.Clear();

            if (_initHandle.IsValid())
            {
                ReleaseHandle(_initHandle);
            }

            _initialized = false;
            Debug.Log("[AddressablesAssetService] ReleaseAll completed.");
        }

        private void SafeRelease(AssetKey key, AsyncOperationHandle handle)
        {
            if (handle.IsValid()) 
                Addressables.Release(handle);

            _entriesMap.Remove(key);
        }

        private static void ReleaseHandle(AsyncOperationHandle handle)
        {
            if (handle.IsValid()) 
                Addressables.Release(handle);
        }

        private class AssetEntry
        {
            public AsyncOperationHandle Handle;
            public int ReferenceCount;

            public AssetEntry(AsyncOperationHandle handle, int referenceCount)
            {
                Handle = handle;
                ReferenceCount = referenceCount;
            }
        }
    }
}
