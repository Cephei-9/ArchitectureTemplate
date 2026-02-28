namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Facade for showing common popups such as error and toast messages.
    /// </summary>
    public interface ICommonPopups
    {
        void ShowError(string message);
        void ShowToast(string message);
    }
}
