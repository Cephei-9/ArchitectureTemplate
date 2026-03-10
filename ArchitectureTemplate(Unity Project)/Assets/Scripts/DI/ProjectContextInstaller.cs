using ArchitectureTemplate.AssetManagement;
using ArchitectureTemplate.UI;
using Zenject;

namespace ArchitectureTemplate.Initialization
{
    /// <summary>
    /// Общий инсталлер для ProjectContext.
    /// Содержит только глобальные биндинги, без привязки к конкретным сценам.
    /// </summary>
    public sealed class ProjectContextInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAssetService>().To<AddressablesAssetService>().AsSingle();
        }
    }
}

