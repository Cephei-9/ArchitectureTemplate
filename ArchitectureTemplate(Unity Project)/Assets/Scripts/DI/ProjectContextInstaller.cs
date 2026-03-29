using ArchitectureTemplate.AssetManagement;
using ArchitectureTemplate.DI;
using ArchitectureTemplate.UI;
using SceneLoading;
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
            
            Container.Bind<LoadingScreenPresenter>().AsTransient();
            Container.BindInterfacesAndSelfTo<ZenjectDiContainer>()
                .AsSingle()
                .WithArguments(Container);
            Container.Bind<SceneLoader>()
                .AsSingle();
            
            Debug.Log("[ProjectContext] Installation completed.");
        }
    }
}

