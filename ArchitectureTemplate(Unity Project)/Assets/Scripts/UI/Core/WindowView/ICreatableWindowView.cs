using System.Threading;

namespace ArchitectureTemplate.UI
{
    public interface ICreatableWindowView<TPresentationModel> : IWindowView where TPresentationModel : IPresentationModel
    {
        void Initialize(TPresentationModel presentationModel, CancellationToken token);
    }
}
