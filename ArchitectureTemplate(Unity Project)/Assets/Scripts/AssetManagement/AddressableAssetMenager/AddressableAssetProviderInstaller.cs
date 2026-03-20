using Zenject;

namespace ArchitectureTemplate.AssetManagement
{
    /// <summary>
    /// Installer that binds the asset module: provider, service and initialization step.
    /// </summary>
    public class AddressableAssetProviderInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AddressablesAssetProvider>()
                .AsSingle();

            Container.Bind<AssetService>()
                .AsSingle();

            Container.Bind<AddressablesInitializationStep>()
                .AsSingle();
        }
    }
}
