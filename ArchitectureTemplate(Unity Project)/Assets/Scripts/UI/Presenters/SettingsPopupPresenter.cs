namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Presenter for the settings popup window.
    /// </summary>
    public class SettingsPopupPresenter : IPresentationModel
    {
        public ArchitectureTemplate.AssetManagement.AssetKey ViewAssetKey => ArchitectureTemplate.AssetManagement.AssetKey.SettingsWindow;

        public void Dispose() { }
    }
}

