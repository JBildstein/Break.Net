using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BreakDotNet.Changes;

namespace BreakDotNet
{
    public partial class TypeComparer
    {
        private IEnumerable<IChange> CheckReferences(Assembly oldAssembly, Assembly newAssembly)
        {
#if NETSTANDARD1_5
            AssemblyName[] oldValues = oldAssembly.GetReferencedAssemblies();
            AssemblyName[] newValues = oldAssembly.GetReferencedAssemblies();
            CompareResult<AssemblyName> compareResult = CompareEnumerables(oldValues, newValues, IsAssemblyEqual);

            return CheckReferenceAdditions(compareResult.Added)
                .Concat(CheckReferenceRemovals(compareResult.Removed))
                .Concat(CheckReferenceMatches(compareResult.Matches));
#else
            return Enumerable.Empty<IChange>();
#endif
        }

        private IEnumerable<IChange> CheckReferenceAdditions(IEnumerable<AssemblyName> values)
        {
            foreach (AssemblyName value in values)
            {
                yield return new ReferenceAddChange(value);
            }
        }

        private IEnumerable<IChange> CheckReferenceRemovals(IEnumerable<AssemblyName> values)
        {
            foreach (AssemblyName value in values)
            {
                yield return new ReferenceRemoveChange(value);
            }
        }

        private IEnumerable<IChange> CheckReferenceMatches(IEnumerable<CompareMatch<AssemblyName>> matches)
        {
            foreach (CompareMatch<AssemblyName> match in matches)
            {
                if (match.OldValue.Version != match.NewValue.Version)
                {
                    yield return new ReferenceVersionChange(match.OldValue, match.NewValue);
                }
            }
        }

        private bool IsAssemblyEqual(AssemblyName oldAssembly, AssemblyName newAssembly)
        {
            return IsNameEqual(oldAssembly.FullName, newAssembly.FullName, !Settings.AssemblyCaseSensitive);
        }
    }
}
