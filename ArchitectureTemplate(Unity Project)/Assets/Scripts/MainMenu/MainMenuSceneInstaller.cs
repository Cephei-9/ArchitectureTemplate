using Game.App;
using Zenject;

namespace ArchitectureTemplate.MainMenu
{
    public sealed class MainMenuSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MainMenuService>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<MainMenuEntryPoint>()
                .AsSingle();
        }
    }
}
