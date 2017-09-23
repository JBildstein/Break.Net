using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Type made abstract change
    /// </summary>
    public class TypeAbstractChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "TYP005";

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
        /// The changed type
        /// </summary>
        public TypeInfo Type { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="TypeAbstractChange"/> class
        /// </summary>
        /// <param name="type">The changed type</param>
        public TypeAbstractChange(TypeInfo type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        /// <summary>
        /// Human readable message about the change
        /// </summary>
        /// <returns>The message about the change</returns>
        public string GetMessage()
        {
            return $"Type {Type.FullName} made abstract";
        }
    }
}
