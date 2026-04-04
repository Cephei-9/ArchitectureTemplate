using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// View for the main menu screen with play and quit actions.
    /// </summary>
    public class MainMenuScreenView : WindowViewBase<MainMenuScreenPresenter>
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;
        
        public override void Initialize(MainMenuScreenPresenter presentationModel, CancellationToken token)
        {
            base.Initialize(presentationModel, token);

            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _quitButton.onClick.AddListener(OnQuitButtonClicked);
        }

        public override void Close()
        {
            InvokeOnClosedEvent();
        }

        private void OnPlayButtonClicked()
        {
            PresentationModel.PlayGame();
        }

        private void OnQuitButtonClicked()
        {
            PresentationModel.Quit();
        }
    }
}
