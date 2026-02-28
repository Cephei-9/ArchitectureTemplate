namespace ArchitectureTemplate.UI
{
    /// <summary>
    /// Arguments for message-based popups (error, toast).
    /// </summary>
    public readonly struct MessageArgs
    {
        public readonly string Text;

        public MessageArgs(string text)
        {
            Text = text;
        }
    }
}
