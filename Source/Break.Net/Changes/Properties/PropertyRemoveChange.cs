using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Removed property change
    /// </summary>
    public class PropertyRemoveChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "PRO004";

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
        /// The type that contains the property
        /// </summary>
        public TypeInfo Parent { get; }
        /// <summary>
        /// The removed property
        /// </summary>
        public PropertyInfo Property { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="PropertyRemoveChange"/> class
        /// </summary>
        /// <param name="parent">The type that contains the property</param>
        /// <param name="property">The removed property</param>
        public PropertyRemoveChange(TypeInfo parent, PropertyInfo property)
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
            return $"Property {Property.Name} of type {Parent.FullName} got removed";
        }
    }
}
