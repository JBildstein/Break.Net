using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Type made concrete change
    /// </summary>
    public class TypeConcreteChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "TYP006";

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
        /// The changed type
        /// </summary>
        public TypeInfo Type { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="TypeConcreteChange"/> class
        /// </summary>
        /// <param name="type">The changed type</param>
        public TypeConcreteChange(TypeInfo type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        /// <summary>
        /// Human readable message about the change
        /// </summary>
        /// <returns>The message about the change</returns>
        public string GetMessage()
        {
            return $"Type {Type.FullName} made concrete";
        }
    }
}
