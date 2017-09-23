using System.Collections.Generic;
using System.Reflection;
using BreakDotNet.Changes;

namespace BreakDotNet
{
    public partial class TypeComparer
    {
        private IEnumerable<IChange> CheckTypeAdditions(IEnumerable<TypeInfo> values)
        {
            foreach (TypeInfo value in values)
            {
                yield return new TypeAddChange(value);
            }
        }

        private IEnumerable<IChange> CheckTypeRemovals(IEnumerable<TypeInfo> values)
        {
            foreach (TypeInfo value in values)
            {
                yield return new TypeRemoveChange(value);
            }
        }

        private IEnumerable<IChange> CheckTypeMatches(IEnumerable<CompareMatch<TypeInfo>> matches)
        {
            var changes = new List<IChange>();
            foreach (CompareMatch<TypeInfo> match in matches)
            {
                changes.AddRange(CheckTypeChanges(match));
                changes.AddRange(CheckMethods(match));
                changes.AddRange(CheckProperties(match));
                changes.AddRange(CheckFields(match));
                changes.AddRange(CheckEvents(match));
                changes.AddRange(CheckConstructors(match));
            }

            return changes;
        }

        private IEnumerable<IChange> CheckTypeChanges(CompareMatch<TypeInfo> match)
        {
            var changes = new List<IChange>();

            if (match.OldValue.IsClass != match.NewValue.IsClass ||
                match.OldValue.IsValueType != match.NewValue.IsValueType ||
                match.OldValue.IsInterface != match.NewValue.IsInterface ||
                match.OldValue.IsEnum != match.NewValue.IsEnum)
            {
                changes.Add(new TypeBaseChange(match.OldValue, match.NewValue));
            }

            if (match.OldValue.IsSealed != match.NewValue.IsSealed)
            {
                if (match.OldValue.IsSealed) { changes.Add(new TypeUnsealedChange(match.NewValue)); }
                else { changes.Add(new TypeSealedChange(match.NewValue)); }
            }

            if (match.OldValue.IsAbstract != match.NewValue.IsAbstract)
            {
                if (match.OldValue.IsAbstract) { changes.Add(new TypeConcreteChange(match.NewValue)); }
                else { changes.Add(new TypeAbstractChange(match.NewValue)); }
            }

            return changes;
        }
    }
}
