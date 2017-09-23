using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Field type change
    /// </summary>
    public class FieldTypeChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "FIE001";

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
        /// The old field
        /// </summary>
        public FieldInfo OldField { get; }
        /// <summary>
        /// The new field
        /// </summary>
        public FieldInfo NewField { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="FieldTypeChange"/> class
        /// </summary>
        /// <param name="parent">The type that contains the field</param>
        /// <param name="oldField">The old field</param>
        /// <param name="newField">The new field</param>
        public FieldTypeChange(TypeInfo parent, FieldInfo oldField, FieldInfo newField)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            OldField = oldField ?? throw new ArgumentNullException(nameof(oldField));
            NewField = newField ?? throw new ArgumentNullException(nameof(newField));
        }

        /// <summary>
        /// Human readable message about the change
        /// </summary>
        /// <returns>The message about the change</returns>
        public string GetMessage()
        {
            return $"Field {NewField.Name} of type {Parent.FullName} changed type of" +
                $" value from {OldField.FieldType.FullName} to {NewField.FieldType.FullName}";
        }
    }
}
