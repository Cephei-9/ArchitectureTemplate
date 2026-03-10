using System;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Presenter for the settings popup window.
    /// </summary>
    public class SettingsPopupPresenter : IPresentationModel
    {
        public AssetId ViewAssetKey => AssetId.Ui_SettingsPopup;

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }
    }
}

