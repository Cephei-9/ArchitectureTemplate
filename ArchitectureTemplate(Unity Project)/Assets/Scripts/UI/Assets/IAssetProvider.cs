using UnityEngine;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Provides loading of prefabs and assets by identifier.
    /// </summary>
    public interface IAssetProvider
    {
        T LoadPrefab<T>(AssetId id) where T : Object;
    }
}
