using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Added property change
    /// </summary>
    public class PropertyAddChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "PRO007";

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
        /// The type that contains the property
        /// </summary>
        public TypeInfo Parent { get; }
        /// <summary>
        /// The added property
        /// </summary>
        public PropertyInfo Property { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="PropertyAddChange"/> class
        /// </summary>
        /// <param name="parent">The type that contains the property</param>
        /// <param name="property">The added property</param>
        public PropertyAddChange(TypeInfo parent, PropertyInfo property)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            Property = property ?? throw new ArgumentNullException(nameof(property));
        }

        /// <summary>
        /// Human readable message about the change
        /// </summary>
        /// <returns>The message about the change</returns>
        public string GetMessage()
        {
            return $"New property {Property.Name} for type {Parent.FullName} added";
        }
    }
}
