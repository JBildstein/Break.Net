using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BreakDotNet.Changes;

namespace BreakDotNet
{
    public partial class TypeComparer
    {
        private IEnumerable<IChange> CheckMethods(CompareMatch<TypeInfo> match)
        {
            IEnumerable<MethodInfoGroup> oldValues = GetMethods(match.OldValue.DeclaredMethods);
            IEnumerable<MethodInfoGroup> newValues = GetMethods(match.NewValue.DeclaredMethods);
            CompareResult<MethodInfoGroup> compareResult = CompareEnumerables(oldValues, newValues, IsMethodGroupEqual);

            return CheckMethodAdditions(match.NewValue, compareResult.Added)
                .Concat(CheckMethodRemovals(match.OldValue, compareResult.Removed))
                .Concat(CheckMethodMatches(match.NewValue, compareResult.Matches));
        }

        private IEnumerable<MethodInfoGroup> GetMethods(IEnumerable<MethodInfo> methods)
        {
            return methods.Where(t => t.IsPublic && !t.IsSpecialName)
                .GroupBy(t => t.Name)
                .Select(t => new MethodInfoGroup(t.Key, t));
        }

        private IEnumerable<IChange> CheckMethodAdditions(TypeInfo parent, IEnumerable<MethodInfoGroup> values)
        {
            var changes = new List<IChange>();
            foreach (MethodInfoGroup value in values)
            {
                changes.AddRange(CheckMethodAdditions(parent, value.Methods));
            }

            return changes;
        }

        private IEnumerable<IChange> CheckMethodRemovals(TypeInfo parent, IEnumerable<MethodInfoGroup> values)
        {
            var changes = new List<IChange>();
            foreach (MethodInfoGroup value in values)
            {
                changes.AddRange(CheckMethodRemovals(parent, value.Methods));
            }

            return changes;
        }

        private IEnumerable<IChange> CheckMethodMatches(TypeInfo parent, IEnumerable<CompareMatch<MethodInfoGroup>> matches)
        {
            var changes = new List<IChange>();
            foreach (CompareMatch<MethodInfoGroup> match in matches)
            {
                CompareResult<MethodInfo> compareResult = CompareEnumerables(match.OldValue.Methods, match.NewValue.Methods, IsMethodParameterCountEqual);

                changes.AddRange(CheckMethodAdditions(parent, compareResult.Added));
                changes.AddRange(CheckMethodRemovals(parent, compareResult.Removed));

                if (compareResult.Matches.Any())
                {
                    // quick check if there is only one item or more without using Count()
                    bool isSingleItem = !compareResult.Matches.Skip(1).Any();
                    if (isSingleItem) { changes.AddRange(CheckMethodChanges(parent, compareResult.Matches.First())); }
                    else
                    {
                        IEnumerable<MethodInfo> oldValues = compareResult.Matches.Select(t => t.OldValue);
                        IEnumerable<MethodInfo> newValues = compareResult.Matches.Select(t => t.NewValue);
                        CompareResult<MethodInfo> typeCompareResult = CompareEnumerables(oldValues, newValues, IsMethodParameterTypesEqual);

                        changes.AddRange(CheckMethodAdditions(parent, typeCompareResult.Added));
                        changes.AddRange(CheckMethodRemovals(parent, typeCompareResult.Removed));

                        foreach (CompareMatch<MethodInfo> typeMatch in typeCompareResult.Matches)
                        {
                            changes.AddRange(CheckMethodChanges(parent, typeMatch));
                        }
                    }
                }
            }

            return changes;
        }

        private IEnumerable<IChange> CheckMethodAdditions(TypeInfo parent, IEnumerable<MethodInfo> values)
        {
            foreach (MethodInfo value in values)
            {
                yield return new MethodAddChange(parent, value);
            }
        }

        private IEnumerable<IChange> CheckMethodRemovals(TypeInfo parent, IEnumerable<MethodInfo> values)
        {
            foreach (MethodInfo value in values)
            {
                yield return new MethodRemoveChange(parent, value);
            }
        }

        private IEnumerable<IChange> CheckMethodChanges(TypeInfo parent, CompareMatch<MethodInfo> value)
        {
            var changes = new List<IChange>();

            if (!IsTypeEqual(value.OldValue.ReturnType, value.NewValue.ReturnType))
            {
                changes.Add(new MethodReturnTypeChange(parent, value.OldValue, value.NewValue));
            }

            ParameterInfo[] oldParams = value.OldValue.GetParameters();
            ParameterInfo[] newParams = value.NewValue.GetParameters();

            for (int i = 0; i < oldParams.Length; i++)
            {
                if (HasParameterTypeChanged(oldParams[i], newParams[i]))
                {
                    changes.Add(new MethodParameterTypeChange(parent, value.NewValue, oldParams[i], newParams[i]));
                }

                if (HasParameterNameChanged(oldParams[i], newParams[i]))
                {
                    changes.Add(new MethodParameterNameChange(parent, value.NewValue, oldParams[i], newParams[i]));
                }

                if (HasParameterMetaChanged(oldParams[i], newParams[i]))
                {
                    changes.Add(new MethodParameterMetaChange(parent, value.NewValue, oldParams[i], newParams[i]));
                }

                if (HasParameterDefaultValueChanged(oldParams[i], newParams[i]))
                {
                    changes.Add(new MethodParameterDefaultValueChange(parent, value.NewValue, oldParams[i], newParams[i]));
                }
            }

            return changes;
        }


        private bool IsMethodGroupEqual(MethodInfoGroup oldValue, MethodInfoGroup newValue)
        {
            return IsTypeOrMemberNameEqual(oldValue.Name, newValue.Name);
        }
    }
}
