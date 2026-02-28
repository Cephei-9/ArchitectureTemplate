using Cysharp.Threading.Tasks;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Orchestrates main menu and settings window opening and closing.
    /// </summary>
    public class MainMenuFlow : IMainMenuFlow
    {
        private readonly IWindowService _windows;

        public MainMenuFlow(IWindowService windows)
        {
            _windows = windows;
        }

        public void OpenMainMenu()
        {
            _windows.Open(WindowId.MainMenu);
        }

        public void OpenSettings()
        {
            _windows.Open(WindowId.SettingsPopup);
        }

        public UniTask CloseSettingsAsync()
        {
            return _windows.CloseAsync(WindowId.SettingsPopup);
        }
    }
}
