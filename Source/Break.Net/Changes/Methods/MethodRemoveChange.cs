using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Removed method change
    /// </summary>
    public class MethodRemoveChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "MET002";

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
        /// The type that contains the method
        /// </summary>
        public TypeInfo Parent { get; }
        /// <summary>
        /// The removed method
        /// </summary>
        public MethodInfo Method { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="MethodRemoveChange"/> class
        /// </summary>
        /// <param name="parent">The type that contains the method</param>
        /// <param name="method">The removed method</param>
        public MethodRemoveChange(TypeInfo parent, MethodInfo method)
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
            return $"Method {Method.Name} of type {Parent.FullName} got removed";
        }
    }
}
