using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Type basis change
    /// </summary>
    public class TypeBaseChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "TYP007";

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
        /// The old type
        /// </summary>
        public TypeInfo OldType { get; }
        /// <summary>
        /// The new type
        /// </summary>
        public TypeInfo NewType { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="TypeBaseChange"/> class
        /// </summary>
        /// <param name="oldType">The old type</param>
        /// <param name="newType">The new type</param>
        public TypeBaseChange(TypeInfo oldType, TypeInfo newType)
        {
            OldType = oldType ?? throw new ArgumentNullException(nameof(oldType));
            NewType = newType ?? throw new ArgumentNullException(nameof(newType));
        }

        /// <summary>
        /// Human readable message about the change
        /// </summary>
        /// <returns>The message about the change</returns>
        public string GetMessage()
        {
            return $"Type {NewType.FullName} changed implementation type";
        }
    }
}
