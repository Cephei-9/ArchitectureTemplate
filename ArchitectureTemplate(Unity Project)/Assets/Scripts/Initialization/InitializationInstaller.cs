using ArchitectureTemplate.UI;
using Initialization.InitializationPipeline;
using Zenject;

namespace ArchitectureTemplate.Initialization
{
    /// <summary>
    /// Installer for the initialization application module.
    /// </summary>
    public class InitializationInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Install<InitializationPipelineInstaller>();
            
            Container.Bind<InitializationScreenPresenter>().AsTransient();
            Container.Bind<ProjectAssetsInitializationStep>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameInitialization>().AsSingle();
        }
    }
}
