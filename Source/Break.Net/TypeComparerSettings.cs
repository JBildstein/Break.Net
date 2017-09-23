namespace BreakDotNet
{
    /// <summary>
    /// Settings for the <see cref="TypeComparer"/> class
    /// </summary>
    public class TypeComparerSettings
    {
        /// <summary>
        /// When true, comparison of type and member names is case insensitive
        /// </summary>
        public bool IgnoreCase { get; set; }
        /// <summary>
        /// When true, comparison of assembly names is case sensitive
        /// </summary>
        public bool AssemblyCaseSensitive { get; set; }
    }
}
