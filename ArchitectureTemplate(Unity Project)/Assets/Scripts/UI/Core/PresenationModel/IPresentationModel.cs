using System;
using ArchitectureTemplate.AssetManagement;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Base presentation model contract for UI windows.
    /// Provides asset key for resolving associated view and supports disposal.
    /// </summary>
    public interface IPresentationModel : IDisposable
    {
        AssetKey ViewAssetKey { get; }
    }
}
