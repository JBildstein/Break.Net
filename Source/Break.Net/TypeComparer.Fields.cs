using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BreakDotNet.Changes;

namespace BreakDotNet
{
    public partial class TypeComparer
    {
        private IEnumerable<IChange> CheckFields(CompareMatch<TypeInfo> match)
        {
            IEnumerable<FieldInfo> oldValues = match.OldValue.DeclaredFields.Where(t => t.IsPublic);
            IEnumerable<FieldInfo> newValues = match.NewValue.DeclaredFields.Where(t => t.IsPublic);
            CompareResult<FieldInfo> compareResult = CompareEnumerables(oldValues, newValues, IsMemberEqual);

            return CheckFieldAdditions(match.NewValue, compareResult.Added)
                .Concat(CheckFieldRemovals(match.OldValue, compareResult.Removed))
                .Concat(CheckFieldMatches(match.NewValue, compareResult.Matches));
        }

        private IEnumerable<IChange> CheckFieldAdditions(TypeInfo parent, IEnumerable<FieldInfo> values)
        {
            foreach (FieldInfo value in values)
            {
                yield return new FieldAddChange(parent, value);
            }
        }

        private IEnumerable<IChange> CheckFieldRemovals(TypeInfo parent, IEnumerable<FieldInfo> values)
        {
            foreach (FieldInfo value in values)
            {
                yield return new FieldRemoveChange(parent, value);
            }
        }

        private IEnumerable<IChange> CheckFieldMatches(TypeInfo parent, IEnumerable<CompareMatch<FieldInfo>> matches)
        {
            var changes = new List<IChange>();
            foreach (CompareMatch<FieldInfo> match in matches)
            {
                if (match.OldValue.IsLiteral != match.NewValue.IsLiteral)
                {
                    if (match.OldValue.IsLiteral) { changes.Add(new FieldConstantRemoveChange(parent, match.NewValue)); }
                    else { changes.Add(new FieldConstantAddChange(parent, match.NewValue)); }
                }

                if (match.OldValue.IsInitOnly != match.NewValue.IsInitOnly)
                {
                    if (match.OldValue.IsInitOnly) { changes.Add(new FieldReadonlyRemoveChange(parent, match.NewValue)); }
                    else { changes.Add(new FieldReadonlyAddChange(parent, match.NewValue)); }
                }

                if (!IsTypeEqual(match.OldValue.FieldType, match.NewValue.FieldType))
                {
                    changes.Add(new FieldTypeChange(parent, match.OldValue, match.NewValue));
                }
            }

            return changes;
        }
    }
}
