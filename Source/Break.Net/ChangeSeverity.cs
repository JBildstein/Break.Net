namespace BreakDotNet
{
    /// <summary>
    /// Severity for a change
    /// </summary>
    public enum ChangeSeverity
    {
        /// <summary>
        /// Major (breaking) change
        /// </summary>
        Major = 3,
        /// <summary>
        /// Minor (feature) change
        /// </summary>
        Minor = 2,
        /// <summary>
        /// Patch (bugfix) change
        /// </summary>
        Patch = 1,
    }
}
