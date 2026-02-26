using Zenject;

namespace ArchitectureTemplate.Initialization
{
    /// <summary>
    /// Общий инсталлер для ProjectContext.
    /// Подтягивает инсталлер системы инициализации.
    /// </summary>
    public sealed class ProjectContextInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Подключаем инсталлер инициализации как часть общего контекста
            Container.Install<InitializationInstaller>();
        }
    }
}

