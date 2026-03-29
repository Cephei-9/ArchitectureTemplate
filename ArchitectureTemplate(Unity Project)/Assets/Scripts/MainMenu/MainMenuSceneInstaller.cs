using Game.App;
using ArchitectureTemplate.UI;
using Zenject;

namespace ArchitectureTemplate.MainMenu
{
    /// <summary>
    /// Installer for the main menu scene.
    /// </summary>
    public sealed class MainMenuSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MainMenuService>()
                .AsSingle();

            Container.Bind<MainMenuAssetsLoader>()
                .AsSingle();

            Container.Bind<MainMenuScreenPresenter>()
                .AsTransient();
        }
    }
}
