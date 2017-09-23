using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Type sealed change
    /// </summary>
    public class TypeSealedChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "TYP004";

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
        /// Creates a new instance of the <see cref="TypeSealedChange"/> class
        /// </summary>
        /// <param name="type">The changed type</param>
        public TypeSealedChange(TypeInfo type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        /// <summary>
        /// Human readable message about the change
        /// </summary>
        /// <returns>The message about the change</returns>
        public string GetMessage()
        {
            return $"Type {Type.FullName} got sealed";
        }
    }
}
