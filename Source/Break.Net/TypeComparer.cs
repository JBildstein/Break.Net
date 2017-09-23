using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BreakDotNet
{
    /// <summary>
    /// Provides methods to compare types of two different assembly versions
    /// </summary>
    public partial class TypeComparer
    {
        /// <summary>
        /// Comparison settings
        /// </summary>
        protected TypeComparerSettings Settings { get; }

        /// <summary>
        /// Flag if the types come from an assembly loaded as reflection-only.
        /// Is potentially set at any time during the comparison.
        /// </summary>
        protected bool IsReflectionOnly = false;

        /// <summary>
        /// Creates a new instance of the <see cref="TypeComparer"/> class with default settings
        /// </summary>
        public TypeComparer()
            : this(new TypeComparerSettings())
        { }

        /// <summary>
        /// Creates a new instance of the <see cref="TypeComparer"/> class
        /// </summary>
        /// <param name="settings">The comparison settings</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="settings"/> is null</exception>
        public TypeComparer(TypeComparerSettings settings)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <summary>
        /// Compares the types of two assemblies for changes
        /// </summary>
        /// <param name="oldAssembly">The old assembly</param>
        /// <param name="newAssembly">The new assembly</param>
        /// <returns>A list of changes between the provided assemblies</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="oldAssembly"/> or <paramref name="newAssembly"/> is null</exception>
        public IEnumerable<IChange> Compare(Assembly oldAssembly, Assembly newAssembly)
        {
            if (oldAssembly == null) { throw new ArgumentNullException(nameof(oldAssembly)); }
            if (newAssembly == null) { throw new ArgumentNullException(nameof(newAssembly)); }

#if NETSTANDARD2_0
            IsReflectionOnly = oldAssembly.ReflectionOnly || newAssembly.ReflectionOnly;
#endif

            IEnumerable<TypeInfo> oldTypes = oldAssembly.DefinedTypes.Where(t => t.IsPublic);
            IEnumerable<TypeInfo> newTypes = newAssembly.DefinedTypes.Where(t => t.IsPublic);

            return CompareBase(oldTypes, newTypes)
                .Concat(CheckReferences(oldAssembly, newAssembly))
                .ToList();
        }

        /// <summary>
        /// Compares the provided types for changes
        /// </summary>
        /// <param name="oldTypes">The old types</param>
        /// <param name="newTypes">The new types</param>
        /// <returns>A list of changes between the provided types</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="oldTypes"/>, <paramref name="newTypes"/> or any of their values is null</exception>
        public IEnumerable<IChange> Compare(IEnumerable<Type> oldTypes, IEnumerable<Type> newTypes)
        {
            if (oldTypes == null || oldTypes.Any(t => t == null)) { throw new ArgumentNullException(nameof(oldTypes)); }
            if (newTypes == null || newTypes.Any(t => t == null)) { throw new ArgumentNullException(nameof(newTypes)); }

            return CompareBase(oldTypes.Select(t => t.GetTypeInfo()), newTypes.Select(t => t.GetTypeInfo())).ToList();
        }

        /// <summary>
        /// Compares the provided types for changes
        /// </summary>
        /// <param name="oldTypes">The old types</param>
        /// <param name="newTypes">The new types</param>
        /// <returns>A list of changes between the provided types</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="oldTypes"/>, <paramref name="newTypes"/> or any of their values is null</exception>
        public IEnumerable<IChange> Compare(IEnumerable<TypeInfo> oldTypes, IEnumerable<TypeInfo> newTypes)
        {
            if (oldTypes == null || oldTypes.Any(t => t == null)) { throw new ArgumentNullException(nameof(oldTypes)); }
            if (newTypes == null || newTypes.Any(t => t == null)) { throw new ArgumentNullException(nameof(newTypes)); }

            return CompareBase(oldTypes, newTypes).ToList();
        }

        private IEnumerable<IChange> CompareBase(IEnumerable<TypeInfo> oldTypes, IEnumerable<TypeInfo> newTypes)
        {
            CompareResult<TypeInfo> compareResult = CompareEnumerables(oldTypes, newTypes, IsTypeInfoEqual);

            return CheckTypeAdditions(compareResult.Added)
                .Concat(CheckTypeRemovals(compareResult.Removed))
                .Concat(CheckTypeMatches(compareResult.Matches));
        }

        /// <summary>
        /// Gets a recommended new version for the given assembly depending on the provided changes
        /// </summary>
        /// <param name="oldAssembly">The old assembly</param>
        /// <param name="changes">The changes between the old and the new assembly</param>
        /// <returns>The recommended new version</returns>
        public static Version GetNewVersion(Assembly oldAssembly, IEnumerable<IChange> changes)
        {
            if (oldAssembly == null) { throw new ArgumentNullException(nameof(oldAssembly)); }

            return GetNewVersion(oldAssembly.GetName().Version, changes);
        }

        /// <summary>
        /// Gets a recommended new version for the given version depending on the provided changes
        /// </summary>
        /// <param name="oldVersion">The old version</param>
        /// <param name="changes">The changes between the old and the new types</param>
        /// <returns>The recommended new version</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="oldVersion"/> or <paramref name="changes"/> is null</exception>
        /// <exception cref="InvalidOperationException">Thrown if biggest change severity is an invalid value</exception>
        public static Version GetNewVersion(Version oldVersion, IEnumerable<IChange> changes)
        {
            if (oldVersion == null) { throw new ArgumentNullException(nameof(oldVersion)); }
            if (changes == null) { throw new ArgumentNullException(nameof(changes)); }

            ChangeSeverity severity;
            if (!changes.Any()) { severity = ChangeSeverity.Patch; }
            else { severity = changes.Max(t => t?.Severity) ?? ChangeSeverity.Patch; }

            switch (severity)
            {
                case ChangeSeverity.Major:
                    return new Version(oldVersion.Major + 1, 0, 0);
                case ChangeSeverity.Minor:
                    return new Version(oldVersion.Major, oldVersion.Minor + 1, 0);
                case ChangeSeverity.Patch:
                    return new Version(oldVersion.Major, oldVersion.Minor, oldVersion.Build + 1);

                default:
                    throw new InvalidOperationException($"Invalid change severity of {severity}");
            }
        }
    }
}
