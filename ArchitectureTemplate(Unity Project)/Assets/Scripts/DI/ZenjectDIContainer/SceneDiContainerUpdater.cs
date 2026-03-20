using UnityEngine;
using Zenject;

namespace ArchitectureTemplate.DI
{
    /// <summary>
    /// Scene installer that updates global ZenjectDiContainer with current scene container.
    /// </summary>
    public class SceneDiContainerUpdater : Installer
    {
        public override void InstallBindings()
        {
            ZenjectDiContainer globalContainer = Container.Resolve<ZenjectDiContainer>();
            globalContainer.UpdateContainer(Container);

            Debug.Log("[SceneDiContainerUpdater] Scene DI container updated.");
        }
    }
}

