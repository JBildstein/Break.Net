namespace BreakDotNet
{
    /// <summary>
    /// Defines a change between two types or assembly version
    /// </summary>
    public interface IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        string Id { get; }
        /// <summary>
        /// Severity of the change
        /// </summary>
        ChangeSeverity Severity { get; }

        /// <summary>
        /// Human readable message about the change
        /// </summary>
        /// <returns>The message about the change</returns>
        string GetMessage();
    }
}
