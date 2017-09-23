using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Added reference change
    /// </summary>
    public class ReferenceAddChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "REF004";

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
            get { return ChangeSeverity.Patch; }
        }
        /// <summary>
        /// The added assembly
        /// </summary>
        public AssemblyName Assembly { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="ReferenceAddChange"/> class
        /// </summary>
        /// <param name="assembly">The added assembly</param>
        public ReferenceAddChange(AssemblyName assembly)
        {
            Assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
        }

        /// <summary>
        /// Human readable message about the change
        /// </summary>
        /// <returns>The message about the change</returns>
        public string GetMessage()
        {
            return $"Reference {Assembly.FullName}, version {Assembly.Version} got added";
        }
    }
}
