using Game.App;
using Zenject;

namespace ArchitectureTemplate.Gameplay
{
    public sealed class GameplaySceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameplayService>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<GameplayStart>()
                .AsSingle();
        }
    }
}
