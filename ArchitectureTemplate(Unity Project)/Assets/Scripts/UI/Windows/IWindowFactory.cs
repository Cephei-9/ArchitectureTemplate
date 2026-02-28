namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Creates window instances from prefabs with dependency injection.
    /// </summary>
    public interface IWindowFactory
    {
        WindowHandle Create(WindowId id, object args);
    }
}
