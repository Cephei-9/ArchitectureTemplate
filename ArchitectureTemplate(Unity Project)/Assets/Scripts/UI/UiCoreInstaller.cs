using UnityEngine;
using Zenject;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Scene installer for UI framework. Binds asset provider, window factory, service and flows.
    /// Requires ResourcesAssetProvider and WindowLinks assigned in inspector.
    /// </summary>
    public class UiCoreInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<WindowFactory>().AsSingle();
            Container.Bind<WindowService>().AsSingle();

            UIRoot uiRoot = Object.FindFirstObjectByType<UIRoot>();
            Container.Bind<UIRoot>()
                .FromInstance(uiRoot)
                .AsSingle();

            // Container.Bind<UIRoot>()
            //     .FromFactory<UIRootFactory>()
            //     .AsSingle()
            //     .NonLazy();
        }
    }
}
