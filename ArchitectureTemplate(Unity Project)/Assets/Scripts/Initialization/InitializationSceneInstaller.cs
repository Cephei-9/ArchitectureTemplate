using Zenject;

namespace ArchitectureTemplate.Initialization
{
    /// <summary>
    /// MonoInstaller для SceneContext сцены инициализации.
    /// Подключает систему инициализации (пайплайн + стартовую точку).
    /// </summary>
    public sealed class InitializationSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Install<InitializationInstaller>();
        }
    }
}

