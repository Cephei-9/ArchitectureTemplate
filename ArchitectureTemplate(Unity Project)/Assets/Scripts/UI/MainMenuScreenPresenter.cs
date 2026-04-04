using System.Threading;
using ArchitectureTemplate.AssetManagement;
using Cysharp.Threading.Tasks;
using Game.App;
using UniRx;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Presentation model for the main menu screen.
    /// </summary>
    public class MainMenuScreenPresenter : IDefaultPresentationModel
    {
        private readonly MainMenuService _mainMenuService;
        private readonly WindowService _windowService;

        public AssetKey ViewAssetKey => AssetKey.MainMenuScreen;

        public MainMenuScreenPresenter(MainMenuService mainMenuService)
        {
            _mainMenuService = mainMenuService;
        }

        public void Initialize(CancellationToken token) { }

        public void PlayGame()
        {
            _mainMenuService.StartGameAsync().Forget();
        }

        public void Quit()
        {
            _mainMenuService.QuitGameAsync();
        }

        public void Dispose() { }
    }
}
