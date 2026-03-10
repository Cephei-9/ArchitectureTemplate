using System;
using ArchitectureTemplate.AssetManagement;

namespace ArchitectureTemplate.UI
{
    public interface IPresentationModel : IDisposable
    {
        AssetKey ViewAssetKey { get; }
    }
}
