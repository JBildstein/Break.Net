using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Constructor parameter type change
    /// </summary>
    public class ConstructorParameterTypeChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "CTR004";

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
        /// The type that contains the constructor
        /// </summary>
        public TypeInfo Parent { get; }
        /// <summary>
        /// The changed constructor
        /// </summary>
        public ConstructorInfo Constructor { get; }
        /// <summary>
        /// The old parameter
        /// </summary>
        public ParameterInfo OldParameter { get; }
        /// <summary>
        /// The new parameter
        /// </summary>
        public ParameterInfo NewParameter { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="ConstructorParameterTypeChange"/> class
        /// </summary>
        /// <param name="parent">The type that contains the constructor</param>
        /// <param name="constructor">The changed constructor</param>
        /// <param name="oldParameter">The old parameter</param>
        /// <param name="newParameter">The new parameter</param>
        public ConstructorParameterTypeChange(TypeInfo parent, ConstructorInfo constructor, ParameterInfo oldParameter, ParameterInfo newParameter)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            Constructor = constructor ?? throw new ArgumentNullException(nameof(constructor));
            OldParameter = oldParameter ?? throw new ArgumentNullException(nameof(oldParameter));
            NewParameter = newParameter ?? throw new ArgumentNullException(nameof(newParameter));
        }

        /// <summary>
        /// Human readable message about the change
        /// </summary>
        /// <returns>The message about the change</returns>
        public string GetMessage()
        {
            return $"Type of parameter {NewParameter.Name} of constructor of type {Parent.FullName} changed" +
                $" from {OldParameter.ParameterType.FullName} to {NewParameter.ParameterType.FullName}";
        }
    }
}
