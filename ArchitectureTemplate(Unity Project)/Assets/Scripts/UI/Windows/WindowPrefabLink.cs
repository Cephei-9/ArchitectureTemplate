using System;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Links window identifier to asset identifier for prefab loading.
    /// </summary>
    [Serializable]
    public class WindowPrefabLink
    {
        public WindowId WindowId;
        public AssetId AssetId;
    }
}
