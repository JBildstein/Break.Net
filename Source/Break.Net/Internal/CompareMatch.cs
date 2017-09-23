using System;

namespace BreakDotNet
{
    /// <summary>
    /// Match between two items
    /// </summary>
    /// <typeparam name="T">Type of items</typeparam>
    internal class CompareMatch<T>
        where T : class
    {
        /// <summary>
        /// The old value
        /// </summary>
        public T OldValue { get; }
        /// <summary>
        /// The new value
        /// </summary>
        public T NewValue { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="CompareMatch{T}"/> class
        /// </summary>
        /// <param name="oldValue">the old value</param>
        /// <param name="newValue">the new value</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="oldValue"/> or <paramref name="newValue"/> is null</exception>
        public CompareMatch(T oldValue, T newValue)
        {
            OldValue = oldValue ?? throw new ArgumentNullException(nameof(oldValue));
            NewValue = newValue ?? throw new ArgumentNullException(nameof(newValue));
        }
    }
}
