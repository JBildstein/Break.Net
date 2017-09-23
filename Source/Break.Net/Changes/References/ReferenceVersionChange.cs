using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Version change of reference
    /// </summary>
    public class ReferenceVersionChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "REF001";

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
        /// The old assembly
        /// </summary>
        public AssemblyName OldAssembly { get; }
        /// <summary>
        /// The new assembly
        /// </summary>
        public AssemblyName NewAssembly { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="ReferenceVersionChange"/> class
        /// </summary>
        /// <param name="oldAssembly">The old assembly</param>
        /// <param name="newAssembly">The new assembly</param>
        public ReferenceVersionChange(AssemblyName oldAssembly, AssemblyName newAssembly)
        {
            OldAssembly = oldAssembly ?? throw new ArgumentNullException(nameof(oldAssembly));
            NewAssembly = newAssembly ?? throw new ArgumentNullException(nameof(newAssembly));
        }

        /// <summary>
        /// Human readable message about the change
        /// </summary>
        /// <returns>The message about the change</returns>
        public string GetMessage()
        {
            return $"Version of reference {NewAssembly.FullName} changed from {OldAssembly.Version} to {NewAssembly.Version}";
        }
    }
}
