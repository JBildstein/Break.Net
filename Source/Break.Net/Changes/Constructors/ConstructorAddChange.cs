using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Added constructor change
    /// </summary>
    public class ConstructorAddChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "CTR006";

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
            get { return ChangeSeverity.Minor; }
        }
        /// <summary>
        /// The type that contains the constructor
        /// </summary>
        public TypeInfo Parent { get; }
        /// <summary>
        /// The added constructor
        /// </summary>
        public ConstructorInfo Constructor { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="ConstructorAddChange"/> class
        /// </summary>
        /// <param name="parent">The type that contains the constructor</param>
        /// <param name="constructor">The added constructor</param>
        public ConstructorAddChange(TypeInfo parent, ConstructorInfo constructor)
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
            return $"New constructor for type {Parent.FullName} added";
        }
    }
}
