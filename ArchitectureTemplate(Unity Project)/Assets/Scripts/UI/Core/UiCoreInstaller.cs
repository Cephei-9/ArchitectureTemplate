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
            Container.Bind<WindowManager>().AsSingle();
            Container.Bind<WindowService>().AsSingle();
            
            Container.BindInstance(Object.FindFirstObjectByType<UIRoot>()).AsSingle();
        }
    }
}
