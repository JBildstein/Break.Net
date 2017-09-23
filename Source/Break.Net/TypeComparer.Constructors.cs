using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BreakDotNet.Changes;

namespace BreakDotNet
{
    public partial class TypeComparer
    {
        private IEnumerable<IChange> CheckConstructors(CompareMatch<TypeInfo> match)
        {
            IEnumerable<ConstructorInfo> oldValues = match.OldValue.DeclaredConstructors.Where(t => t.IsPublic);
            IEnumerable<ConstructorInfo> newValues = match.NewValue.DeclaredConstructors.Where(t => t.IsPublic);
            CompareResult<ConstructorInfo> compareResult = CompareEnumerables(oldValues, newValues, IsMethodParameterCountEqual);

            return CheckConstructorAdditions(match.NewValue, compareResult.Added)
                .Concat(CheckConstructorRemovals(match.OldValue, compareResult.Removed))
                .Concat(CheckConstructorMatches(match.NewValue, compareResult.Matches));
        }

        private IEnumerable<IChange> CheckConstructorAdditions(TypeInfo parent, IEnumerable<ConstructorInfo> values)
        {
            foreach (ConstructorInfo value in values)
            {
                yield return new ConstructorAddChange(parent, value);
            }
        }

        private IEnumerable<IChange> CheckConstructorRemovals(TypeInfo parent, IEnumerable<ConstructorInfo> values)
        {
            foreach (ConstructorInfo value in values)
            {
                yield return new ConstructorRemoveChange(parent, value);
            }
        }

        private IEnumerable<IChange> CheckConstructorMatches(TypeInfo parent, IEnumerable<CompareMatch<ConstructorInfo>> matches)
        {
            var changes = new List<IChange>();

            // quick check if there is only one item or more without using Count()
            bool isSingleItem = matches.Any() && !matches.Skip(1).Any();
            if (isSingleItem) { changes.AddRange(CheckConstructorChanges(parent, matches.First())); }
            else
            {
                IEnumerable<ConstructorInfo> oldValues = matches.Select(t => t.OldValue);
                IEnumerable<ConstructorInfo> newValues = matches.Select(t => t.NewValue);
                CompareResult<ConstructorInfo> typeCompareResult = CompareEnumerables(oldValues, newValues, IsMethodParameterTypesEqual);

                changes.AddRange(CheckConstructorAdditions(parent, typeCompareResult.Added));
                changes.AddRange(CheckConstructorRemovals(parent, typeCompareResult.Removed));

                foreach (CompareMatch<ConstructorInfo> typeMatch in typeCompareResult.Matches)
                {
                    changes.AddRange(CheckConstructorChanges(parent, typeMatch));
                }
            }

            return changes;
        }

        private IEnumerable<IChange> CheckConstructorChanges(TypeInfo parent, CompareMatch<ConstructorInfo> value)
        {
            var changes = new List<IChange>();
            ParameterInfo[] oldParams = value.OldValue.GetParameters();
            ParameterInfo[] newParams = value.NewValue.GetParameters();

            for (int i = 0; i < oldParams.Length; i++)
            {
                if (HasParameterTypeChanged(oldParams[i], newParams[i]))
                {
                    changes.Add(new ConstructorParameterTypeChange(parent, value.NewValue, oldParams[i], newParams[i]));
                }

                if (HasParameterNameChanged(oldParams[i], newParams[i]))
                {
                    changes.Add(new ConstructorParameterNameChange(parent, value.NewValue, oldParams[i], newParams[i]));
                }

                if (HasParameterMetaChanged(oldParams[i], newParams[i]))
                {
                    changes.Add(new ConstructorParameterMetaChange(parent, value.NewValue, oldParams[i], newParams[i]));
                }

                if (HasParameterDefaultValueChanged(oldParams[i], newParams[i]))
                {
                    changes.Add(new ConstructorParameterDefaultValueChange(parent, value.NewValue, oldParams[i], newParams[i]));
                }
            }

            return changes;
        }
    }
}
