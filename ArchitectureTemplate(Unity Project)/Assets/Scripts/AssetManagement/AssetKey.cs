using System;

namespace ArchitectureTemplate.AssetManagement
{
    /// <summary>
    /// Keys for single assets (Addressables Address).
    /// Addressables Address must match the enum member name (ToString()).
    /// Example: AssetKey.MainMenuRoot -> Address "MainMenuRoot".
    /// </summary>
    public enum AssetKey
    {
        InitializationScreen,
        LoadingCurtain,

        SettingsWindow,

        GameplayHud,
    }
}
