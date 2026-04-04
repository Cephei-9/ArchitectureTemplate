using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// View for the gameplay screen with return to main menu action.
    /// </summary>
    public class GameplayScreenView : WindowViewBase<GameplayScreenPresenter>
    {
        [SerializeField] private Button _mainMenuButton;

        public override void Initialize(GameplayScreenPresenter presentationModel, CancellationToken token)
        {
            base.Initialize(presentationModel, token);

            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        }

        public override void Close()
        {
            InvokeOnClosedEvent();
        }

        private void OnMainMenuButtonClicked()
        {
            PresentationModel.ReturnToMainMenu();
        }
    }
}
