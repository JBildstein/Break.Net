using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Property type change
    /// </summary>
    public class PropertyTypeChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "PRO001";

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
        /// The old property
        /// </summary>
        public PropertyInfo OldProperty { get; }
        /// <summary>
        /// The new property
        /// </summary>
        public PropertyInfo NewProperty { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="PropertyTypeChange"/> class
        /// </summary>
        /// <param name="parent">The type that contains the property</param>
        /// <param name="oldProperty">The old property</param>
        /// <param name="newProperty">The new property</param>
        public PropertyTypeChange(TypeInfo parent, PropertyInfo oldProperty, PropertyInfo newProperty)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            OldProperty = oldProperty ?? throw new ArgumentNullException(nameof(oldProperty));
            NewProperty = newProperty ?? throw new ArgumentNullException(nameof(newProperty));
        }

        /// <summary>
        /// Human readable message about the change
        /// </summary>
        /// <returns>The message about the change</returns>
        public string GetMessage()
        {
            return $"Property {NewProperty.Name} of type {Parent.FullName} changed type of" +
                $" value from {OldProperty.PropertyType.FullName} to {NewProperty.PropertyType.FullName}";
        }
    }
}
