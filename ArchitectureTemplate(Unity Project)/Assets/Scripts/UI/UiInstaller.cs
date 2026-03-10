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
        public override void InstallBindings()
        {
            Container.Bind<WindowFactory>().AsSingle();
            Container.Bind<WindowService>().AsSingle();
            
            Container.Bind<MainMenuPresenter>().AsTransient();
            Container.Bind<SettingsPopupPresenter>().AsTransient();
            Container.Bind<ErrorPopupPresentationModel>().AsTransient();
            Container.Bind<ToastPopupPresentationModel>().AsTransient();
            
            Container.Bind<MainMenuFlow>().AsSingle();
            Container.Bind<CommonPopups>().AsSingle();
        }
    }
}
