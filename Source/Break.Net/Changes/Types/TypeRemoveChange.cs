using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Removed type change
    /// </summary>
    public class TypeRemoveChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "TYP001";

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
        /// The removed type
        /// </summary>
        public TypeInfo Type { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="TypeRemoveChange"/> class
        /// </summary>
        /// <param name="type">The removed type</param>
        public TypeRemoveChange(TypeInfo type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        /// <summary>
        /// Human readable message about the change
        /// </summary>
        /// <returns>The message about the change</returns>
        public string GetMessage()
        {
            return $"Type {Type.FullName} got removed";
        }
    }
}
