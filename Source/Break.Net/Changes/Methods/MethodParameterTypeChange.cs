using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Method parameter type change
    /// </summary>
    public class MethodParameterTypeChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "MET003";

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
        /// The changed method
        /// </summary>
        public MethodInfo Method { get; }
        /// <summary>
        /// The old parameter
        /// </summary>
        public ParameterInfo OldParameter { get; }
        /// <summary>
        /// The new parameter
        /// </summary>
        public ParameterInfo NewParameter { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="MethodParameterTypeChange"/> class
        /// </summary>
        /// <param name="parent">The type that contains the method</param>
        /// <param name="method">The changed method</param>
        /// <param name="oldParameter">The old parameter</param>
        /// <param name="newParameter">The new parameter</param>
        public MethodParameterTypeChange(TypeInfo parent, MethodInfo method, ParameterInfo oldParameter, ParameterInfo newParameter)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            Method = method ?? throw new ArgumentNullException(nameof(method));
            OldParameter = oldParameter ?? throw new ArgumentNullException(nameof(oldParameter));
            NewParameter = newParameter ?? throw new ArgumentNullException(nameof(newParameter));
        }

        /// <summary>
        /// Human readable message about the change
        /// </summary>
        /// <returns>The message about the change</returns>
        public string GetMessage()
        {
            return $"Type of parameter {NewParameter.Name} of method {Method.Name} of type {Parent.FullName} changed" +
                $" from {OldParameter.ParameterType.FullName} to {NewParameter.ParameterType.FullName}";
        }
    }
}
