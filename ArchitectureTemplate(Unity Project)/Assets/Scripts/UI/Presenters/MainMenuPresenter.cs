namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Presenter for the main menu window.
    /// </summary>
    public class MainMenuPresenter : IPresentationModel
    {
        public ArchitectureTemplate.AssetManagement.AssetKey ViewAssetKey => ArchitectureTemplate.AssetManagement.AssetKey.MainMenuWindow;

        public void Dispose() { }
    }
}

