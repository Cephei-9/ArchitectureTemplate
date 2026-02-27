using ArchitectureTemplate.Initialization.Testing;
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

            Container.Bind<IInitializationPipelineStep>()
                .To<TestPipelineStep>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<GameInitialization>()
                .AsSingle();
        }
    }
}
