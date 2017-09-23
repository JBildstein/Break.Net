using System;
using System.Collections.Generic;
using System.Reflection;

namespace BreakDotNet
{
    /// <summary>
    /// Grouping of several <see cref="MethodInfo"/>s
    /// </summary>
    internal class MethodInfoGroup
    {
        /// <summary>
        /// Name of the method group
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Grouped methods
        /// </summary>
        public IEnumerable<MethodInfo> Methods { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="MethodInfoGroup"/> class
        /// </summary>
        /// <param name="name">the name of the group</param>
        /// <param name="methods">the grouped methods</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> or <paramref name="methods"/> is null</exception>
        public MethodInfoGroup(string name, IEnumerable<MethodInfo> methods)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Methods = methods ?? throw new ArgumentNullException(nameof(methods));
        }
    }
}
