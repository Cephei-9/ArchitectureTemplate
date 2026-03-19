using System.Threading;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Presentation model that requires strongly typed arguments for initialization.
    /// </summary>
    /// <typeparam name="T">Type of initialization argument.</typeparam>
    public interface IArgumentedPresentationModel<T> : IPresentationModel
    {
        void InitializeArgument(T argument, CancellationToken token);
    }
}