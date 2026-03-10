using System;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Presenter for the main menu window.
    /// </summary>
    public class MainMenuPresenter : IPresentationModel
    {
        public AssetId ViewAssetKey => AssetId.Ui_MainMenu;

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }
    }
}

