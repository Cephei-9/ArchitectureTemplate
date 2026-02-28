using System;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Maps asset identifier to Resources path.
    /// </summary>
    [Serializable]
    public class AssetPath
    {
        public AssetId Id;
        public string ResourcesPath;
    }
}
