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
            InitializationPipelineModel model = new();
            InitializationPipelineRunner runner = new(model);
            InitializationPipelineService service = new(model, runner);

            Container.Bind<InitializationPipelineService>()
                .FromInstance(service)
                .AsSingle();
        }
    }
}