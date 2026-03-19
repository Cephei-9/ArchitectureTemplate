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
            Container.Install<AssetManagerInstaller>();
            Container.Install<UiCoreInstaller>();

            Container.BindInterfacesAndSelfTo<ZenjectDiContainer>()
                .AsSingle()
                .WithArguments(Container);

            Debug.Log("Project Context");
        }
    }
}

