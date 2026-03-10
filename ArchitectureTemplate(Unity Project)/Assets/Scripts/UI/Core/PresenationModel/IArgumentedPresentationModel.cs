using System.Threading;

namespace ArchitectureTemplate.UI
{
    public interface IArgumentedPresentationModel<T> : IPresentationModel
    {
        void InitializeArgument(T argument, CancellationToken token);
    }
}