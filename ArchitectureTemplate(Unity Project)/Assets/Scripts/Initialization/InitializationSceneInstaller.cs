using ArchitectureTemplate.DI;
using UnityEngine;
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
            Debug.Log("[InitializationSceneContext] Installation started.");
            
            Container.Install<SceneDiContainerUpdater>();
            Container.Install<InitializationInstaller>();
            
            Debug.Log("[InitializationSceneContext] Installation completed.");
        }
    }
}
