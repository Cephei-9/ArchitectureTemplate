using ArchitectureTemplate.Initialization.Testing;
using Initialization.InitializationPipeline;
using Zenject;

namespace ArchitectureTemplate.Initialization
{
    /// <summary>
    /// Инсталлер системы инициализации (как понятия приложения).
    /// Содержит стартовую точку и тестовые шаги, и подключает пайплайн.
    /// </summary>
    public sealed class InitializationInstaller : Installer
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
