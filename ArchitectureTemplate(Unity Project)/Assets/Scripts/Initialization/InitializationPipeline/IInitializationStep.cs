using System.Threading;
using Cysharp.Threading.Tasks;

namespace Initialization.InitializationPipeline
{
    /// <summary>
    /// Contract for one initialization pipeline step.
    /// </summary>
    public interface IInitializationPipelineStep
    {
        string Name { get; }
        float Weight { get; }
        
        UniTask ExecuteAsync(CancellationToken cancellationToken = default);
    }
}
