using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Removed field change
    /// </summary>
    public class FieldRemoveChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "FIE002";

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
        /// The type that contains the field
        /// </summary>
        public TypeInfo Parent { get; }
        /// <summary>
        /// The removed field
        /// </summary>
        public FieldInfo Field { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="FieldRemoveChange"/> class
        /// </summary>
        /// <param name="parent">The type that contains the field</param>
        /// <param name="field">The removed field</param>
        public FieldRemoveChange(TypeInfo parent, FieldInfo field)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            Field = field ?? throw new ArgumentNullException(nameof(field));
        }

        /// <summary>
        /// Human readable message about the change
        /// </summary>
        /// <returns>The message about the change</returns>
        public string GetMessage()
        {
            return $"Field {Field.Name} of type {Parent.FullName} got removed";
        }
    }
}
