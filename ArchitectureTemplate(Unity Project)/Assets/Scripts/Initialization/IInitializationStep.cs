using System.Threading;
using Cysharp.Threading.Tasks;

namespace ArchitectureTemplate.Initialization
{
    public interface IInitializationStep
    {
        string Name { get; }
        float Weight { get; }
        UniTask ExecuteAsync(CancellationToken cancellationToken);
    }
}
