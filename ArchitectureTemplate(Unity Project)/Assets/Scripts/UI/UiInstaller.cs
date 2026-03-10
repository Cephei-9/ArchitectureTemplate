using Zenject;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Scene installer for UI framework. Binds asset provider, window factory, service and flows.
    /// Requires ResourcesAssetProvider and WindowLinks assigned in inspector.
    /// </summary>
    public class UiInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<WindowFactory>().AsSingle();
            Container.Bind<WindowService>().AsSingle();
            
            Container.BindFactory<UIRoot, UIRootFactory>().AsSingle();
            Container.Bind<UIRoot>()
                .FromFactory<UIRootFactory>()
                .AsSingle()
                .NonLazy();
        }
    }
}
