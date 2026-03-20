using ArchitectureTemplate.AssetManagement;
using ArchitectureTemplate.DI;
using ArchitectureTemplate.UI;
using UnityEngine;
using Zenject;

namespace ArchitectureTemplate.Initialization
{
    /// <summary>
    /// Common installer for ProjectContext.
    /// </summary>
    public class ProjectContextInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Debug.Log("[ProjectContext] Installation started.");

            Container.Install<AddressableAssetProviderInstaller>();
            Container.Install<UiCoreInstaller>();
            Container.BindInterfacesAndSelfTo<ZenjectDiContainer>()
                .AsSingle()
                .WithArguments(Container);

            Debug.Log("[ProjectContext] Installation completed.");
        }
    }
}

