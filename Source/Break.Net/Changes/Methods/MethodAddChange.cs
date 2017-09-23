using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Added method change
    /// </summary>
    public class MethodAddChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "MET007";

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
        /// The type that contains the method
        /// </summary>
        public TypeInfo Parent { get; }
        /// <summary>
        /// The added method
        /// </summary>
        public MethodInfo Method { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="MethodAddChange"/> class
        /// </summary>
        /// <param name="parent">The type that contains the method</param>
        /// <param name="method">The added method</param>
        public MethodAddChange(TypeInfo parent, MethodInfo method)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            Method = method ?? throw new ArgumentNullException(nameof(method));
        }

        /// <summary>
        /// Human readable message about the change
        /// </summary>
        /// <returns>The message about the change</returns>
        public string GetMessage()
        {
            return $"New method {Method.Name} for type {Parent.FullName} added";
        }
    }
}
