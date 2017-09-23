using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Removed constructor change
    /// </summary>
    public class ConstructorRemoveChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "CTR003";

        /// <summary>
        /// Change ID
        /// </summary>
        public string Id
        {
            get { return IdConstant; }
        }
        /// <summary>
        /// Severity of the change
        /// </summary>
        public ChangeSeverity Severity
        {
            get { return ChangeSeverity.Major; }
        }
        /// <summary>
        /// The type that contains the constructor
        /// </summary>
        public TypeInfo Parent { get; }
        /// <summary>
        /// The removed constructor
        /// </summary>
        public ConstructorInfo Constructor { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="ConstructorRemoveChange"/> class
        /// </summary>
        /// <param name="parent">The type that contains the constructor</param>
        /// <param name="constructor">The removed constructor</param>
        public ConstructorRemoveChange(TypeInfo parent, ConstructorInfo constructor)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            Constructor = constructor ?? throw new ArgumentNullException(nameof(constructor));
        }

        /// <summary>
        /// Human readable message about the change
        /// </summary>
        /// <returns>The message about the change</returns>
        public string GetMessage()
        {
            return $"Constructor of type {Parent.FullName} got removed";
        }
    }
}
