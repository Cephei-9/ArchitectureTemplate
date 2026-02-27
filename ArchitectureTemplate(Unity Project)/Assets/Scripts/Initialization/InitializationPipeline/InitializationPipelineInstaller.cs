using Zenject;

namespace Initialization.InitializationPipeline
{
    /// <summary>
    /// Инсталлер пайплайна. Регистрирует модель, раннер и сервис пайплайна.
    /// </summary>
    public sealed class InitializationPipelineInstaller : Installer
    {
        public override void InstallBindings()
        {
            InitializationPipelineModel model = new InitializationPipelineModel();
            InitializationPipelineRunner runner = new InitializationPipelineRunner(model);
            InitializationPipelineService service = new InitializationPipelineService(model, runner);

            Container.Bind<InitializationPipelineService>()
                .FromInstance(service)
                .AsSingle();
        }
    }
}

