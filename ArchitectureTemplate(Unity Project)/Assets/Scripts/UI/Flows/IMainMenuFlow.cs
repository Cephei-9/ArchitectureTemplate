using Cysharp.Threading.Tasks;

namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Application flow for main menu navigation and settings popup.
    /// </summary>
    public interface IMainMenuFlow
    {
        void OpenMainMenu();
        void OpenSettings();
        UniTask CloseSettingsAsync();
    }
}
