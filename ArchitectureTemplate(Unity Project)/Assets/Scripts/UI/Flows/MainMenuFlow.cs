using Cysharp.Threading.Tasks;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Orchestrates main menu and settings window opening and closing.
    /// </summary>
    public class MainMenuFlow
    {
        private readonly WindowService _windows;

        public MainMenuFlow(WindowService windows)
        {
            _windows = windows;
        }

        public void OpenMainMenu()
        {
            _windows.OpenWindow<MainMenuPresenter>(UILayer.Screen, out _);
        }

        public void OpenSettings()
        {
            _windows.OpenWindow<SettingsPopupPresenter>(UILayer.Popups, out _);
        }

        public void CloseSettingsAsync()
        {
            _windows.CloseWindow<SettingsPopupPresenter>();
        }
    }
}
