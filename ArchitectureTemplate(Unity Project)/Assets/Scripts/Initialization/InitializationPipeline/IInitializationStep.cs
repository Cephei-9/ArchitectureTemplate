using System.Threading;
using Cysharp.Threading.Tasks;

namespace Initialization.InitializationPipeline
{
    /// <summary>
    /// Контракт шага пайплайна.
    /// </summary>
    public interface IInitializationPipelineStep
    {
        string Name { get; }
        float Weight { get; }
        
        UniTask ExecuteAsync(CancellationToken cancellationToken);
    }
}
