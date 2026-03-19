using UnityEngine;
using Zenject;

namespace ArchitectureTemplate.AssetManagement
{
    /// <summary>
    /// Installer that binds the global asset service implementation.
    /// </summary>
    public class AssetManagerInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AddressablesAssetService>()
                .AsSingle();

            Debug.Log("Install Asset Menegement");
        }
    }
}

