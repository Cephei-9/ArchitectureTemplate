using System.Threading;
using ArchitectureTemplate.AssetManagement;
using Cysharp.Threading.Tasks;
using Game.App;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Presentation model for the gameplay screen.
    /// </summary>
    public class GameplayScreenPresenter : IDefaultPresentationModel
    {
        private readonly GameplayService _gameplayService;

        public AssetKey ViewAssetKey => AssetKey.GameplayScreen;

        public GameplayScreenPresenter(GameplayService gameplayService)
        {
            _gameplayService = gameplayService;
        }

        public void Initialize(CancellationToken token) { }

        public void ReturnToMainMenu()
        {
            _gameplayService.ReturnToMainMenuAsync().Forget();
        }

        public void Dispose() { }
    }
}
