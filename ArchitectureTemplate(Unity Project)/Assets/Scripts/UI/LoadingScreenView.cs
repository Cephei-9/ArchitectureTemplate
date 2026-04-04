namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// View implementation for the loading screen without additional logic.
    /// </summary>
    public class LoadingScreenView : WindowViewBase<LoadingScreenPresenter>
    {
        public override void Close()
        {
            InvokeOnClosedEvent();
        }
    }
}
