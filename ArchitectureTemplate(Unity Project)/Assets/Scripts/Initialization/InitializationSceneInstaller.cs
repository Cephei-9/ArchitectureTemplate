using ArchitectureTemplate.DI;
using Zenject;

namespace ArchitectureTemplate.Initialization
{
    /// <summary>
    /// Scene installer that wires initialization installers into SceneContext.
    /// </summary>
    public class InitializationSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Install<SceneDiContainerUpdater>();
            Container.Install<InitializationInstaller>();
        }
    }
}
