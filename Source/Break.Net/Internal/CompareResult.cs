using System.Collections.Generic;

namespace BreakDotNet
{
    /// <summary>
    /// Result of a comparison between several items
    /// </summary>
    /// <typeparam name="T">Type of items</typeparam>
    internal class CompareResult<T>
        where T : class
    {
        /// <summary>
        /// Items that have been added
        /// </summary>
        public IEnumerable<T> Added { get; set; }
        /// <summary>
        /// Items that got removed
        /// </summary>
        public IEnumerable<T> Removed { get; set; }
        /// <summary>
        /// Items that matched between old and new version
        /// </summary>
        public IEnumerable<CompareMatch<T>> Matches { get; set; }
    }
}
