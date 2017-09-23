using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Added field change
    /// </summary>
    public class FieldAddChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "FIE006";

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
        /// The type that contains the field
        /// </summary>
        public TypeInfo Parent { get; }
        /// <summary>
        /// The added field
        /// </summary>
        public FieldInfo Field { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="FieldAddChange"/> class
        /// </summary>
        /// <param name="parent">The type that contains the field</param>
        /// <param name="field">The added field</param>
        public FieldAddChange(TypeInfo parent, FieldInfo field)
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
            return $"New field {Field.Name} for type {Parent.FullName} added";
        }
    }
}
