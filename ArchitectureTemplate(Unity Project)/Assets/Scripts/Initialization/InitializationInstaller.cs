using ArchitectureTemplate.Initialization.Testing;
using Zenject;

namespace ArchitectureTemplate.Initialization
{
    /// <summary>
    /// Отдельный инсталлер для системы инициализации.
    /// Регистрирует сервис и единственный тестовый шаг.
    /// </summary>
    public sealed class InitializationInstaller : Installer<InitializationInstaller>
    {
        public override void InstallBindings()
        {
            // Сервис инициализации
            Container.Bind<InitializationService>()
                .AsSingle();

            // Единственный тестовый шаг как реализация IInitializationStep
            Container.Bind<IInitializationStep>()
                .To<TestInitializationStep>()
                .AsSingle();
        }
    }
}

