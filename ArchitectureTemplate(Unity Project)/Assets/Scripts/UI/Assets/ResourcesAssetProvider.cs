using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Loads prefabs from Resources folder using configurable path mapping.
    /// </summary>
    public class ResourcesAssetProvider : MonoBehaviour, IAssetProvider
    {
        [SerializeField] private AssetPath[] _paths;

        private Dictionary<AssetId, string> _pathMap;

        private void Awake()
        {
            _pathMap = new Dictionary<AssetId, string>(_paths.Length);

            foreach (AssetPath path in _paths)
                _pathMap[path.Id] = path.ResourcesPath;
        }

        public T LoadPrefab<T>(AssetId id) where T : Object
        {
            if (!_pathMap.TryGetValue(id, out string path))
                throw new InvalidOperationException($"AssetId not mapped: {id}");

            T asset = Resources.Load<T>(path);

            if (asset == null)
                throw new InvalidOperationException($"Resources asset not found: {id} ({path})");

            return asset;
        }
    }
}
