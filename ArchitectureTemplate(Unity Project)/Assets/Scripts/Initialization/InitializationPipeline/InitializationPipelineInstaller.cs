using Zenject;

namespace Initialization.InitializationPipeline
{
    /// <summary>
    /// Installer for the initialization pipeline model, runner, and service.
    /// </summary>
    public class InitializationPipelineInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<InitializationPipelineModel>().AsSingle();
            Container.Bind<InitializationPipelineRunner>().AsSingle();
            Container.Bind<InitializationPipelineService>().AsSingle();
        }
    }
}