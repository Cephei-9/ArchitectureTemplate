using Game.App;
using ArchitectureTemplate.UI;
using Zenject;

namespace ArchitectureTemplate.Gameplay
{
    /// <summary>
    /// Installer for the gameplay scene.
    /// </summary>
    public sealed class GameplaySceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameplayService>()
                .AsSingle();

            Container.Bind<GameplayAssetsLoader>()
                .AsSingle();

            Container.Bind<GameplayScreenPresenter>()
                .AsTransient();

            Container.BindInterfacesAndSelfTo<GameplayStart>()
                .AsSingle();
        }
    }
}
