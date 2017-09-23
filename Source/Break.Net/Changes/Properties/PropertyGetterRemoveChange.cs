using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Removed getter for property change
    /// </summary>
    public class PropertyGetterRemoveChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "PRO005";

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
        /// The changed property
        /// </summary>
        public PropertyInfo Property { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="PropertyGetterRemoveChange"/> class
        /// </summary>
        /// <param name="parent">The type that contains the property</param>
        /// <param name="property">The changed property</param>
        public PropertyGetterRemoveChange(TypeInfo parent, PropertyInfo property)
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
            return $"Getter for property {Property.Name} of type {Parent.FullName} got removed";
        }
    }
}
