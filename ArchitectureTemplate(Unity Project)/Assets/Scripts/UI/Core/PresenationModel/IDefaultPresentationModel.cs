using System.Threading;

namespace ArchitectureTemplate.UI
{
    public interface IDefaultPresentationModel : IPresentationModel
    {
        void Initialize(CancellationToken token);
    }
}