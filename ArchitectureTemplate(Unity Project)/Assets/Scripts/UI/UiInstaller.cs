using System;
using UnityEngine;
using Zenject;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Scene installer for UI framework. Binds asset provider, window factory, service and flows.
    /// Requires ResourcesAssetProvider and WindowLinks assigned in inspector.
    /// </summary>
    public class UiInstaller : MonoInstaller
    {
        [SerializeField] private ResourcesAssetProvider _resourcesAssetProvider;
        [SerializeField] private WindowLinks _windowLinks;

        public override void InstallBindings()
        {
            if (_resourcesAssetProvider == null)
                throw new InvalidOperationException("ResourcesAssetProvider is not assigned in UiInstaller.");

            if (_windowLinks == null || _windowLinks.Links == null)
                throw new InvalidOperationException("WindowLinks or its Links array is not assigned in UiInstaller.");

            Container.Bind<IAssetProvider>().FromInstance(_resourcesAssetProvider).AsSingle();

            Container.Bind<IWindowFactory>()
                .To<WindowFactory>()
                .AsSingle()
                .WithArguments(_windowLinks.Links);

            Container.Bind<IWindowService>().To<WindowService>().AsSingle();
            Container.Bind<IMainMenuFlow>().To<MainMenuFlow>().AsSingle();
            Container.Bind<ICommonPopups>().To<CommonPopups>().AsSingle();
        }
    }
}
