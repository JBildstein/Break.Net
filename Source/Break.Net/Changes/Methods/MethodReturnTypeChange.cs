using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Method return type change
    /// </summary>
    public class MethodReturnTypeChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "MET001";

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
        /// The old method
        /// </summary>
        public MethodInfo OldMethod { get; }
        /// <summary>
        /// The new method
        /// </summary>
        public MethodInfo NewMethod { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="MethodReturnTypeChange"/> class
        /// </summary>
        /// <param name="parent">The type that contains the method</param>
        /// <param name="oldMethod">The old method</param>
        /// <param name="newMethod">The new method</param>
        public MethodReturnTypeChange(TypeInfo parent, MethodInfo oldMethod, MethodInfo newMethod)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            OldMethod = oldMethod ?? throw new ArgumentNullException(nameof(oldMethod));
            NewMethod = newMethod ?? throw new ArgumentNullException(nameof(newMethod));
        }

        /// <summary>
        /// Human readable message about the change
        /// </summary>
        /// <returns>The message about the change</returns>
        public string GetMessage()
        {
            return $"Method {NewMethod.Name} of type {Parent.FullName} changed return type" +
                $" from {OldMethod.ReturnType.FullName} to {NewMethod.ReturnType.FullName}";
        }
    }
}
