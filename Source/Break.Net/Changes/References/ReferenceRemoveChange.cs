using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Removed reference change
    /// </summary>
    public class ReferenceRemoveChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "REF002";

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
        /// The removed assembly
        /// </summary>
        public AssemblyName Assembly { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="ReferenceRemoveChange"/> class
        /// </summary>
        /// <param name="assembly">The removed assembly</param>
        public ReferenceRemoveChange(AssemblyName assembly)
        {
            Assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
        }

        /// <summary>
        /// Human readable message about the change
        /// </summary>
        /// <returns>The message about the change</returns>
        public string GetMessage()
        {
            return $"Reference {Assembly.FullName} got removed";
        }
    }
}
