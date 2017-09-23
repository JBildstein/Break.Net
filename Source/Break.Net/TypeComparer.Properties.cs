using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BreakDotNet.Changes;

namespace BreakDotNet
{
    public partial class TypeComparer
    {
        private IEnumerable<IChange> CheckProperties(CompareMatch<TypeInfo> match)
        {
            IEnumerable<PropertyInfo> oldValues = GetPublicProperties(match.OldValue.DeclaredProperties);
            IEnumerable<PropertyInfo> newValues = GetPublicProperties(match.NewValue.DeclaredProperties);
            CompareResult<PropertyInfo> compareResult = CompareEnumerables(oldValues, newValues, IsMemberEqual);

            return CheckPropertyAdditions(match.NewValue, compareResult.Added)
                .Concat(CheckPropertyRemovals(match.OldValue, compareResult.Removed))
                .Concat(CheckPropertyMatches(match.NewValue, compareResult.Matches));
        }

        private IEnumerable<PropertyInfo> GetPublicProperties(IEnumerable<PropertyInfo> properties)
        {
            return properties.Where(t => t.GetMethod?.IsPublic == true || t.SetMethod?.IsPublic == true);
        }

        private IEnumerable<IChange> CheckPropertyAdditions(TypeInfo parent, IEnumerable<PropertyInfo> values)
        {
            foreach (PropertyInfo value in values)
            {
                yield return new PropertyAddChange(parent, value);
            }
        }

        private IEnumerable<IChange> CheckPropertyRemovals(TypeInfo parent, IEnumerable<PropertyInfo> values)
        {
            foreach (PropertyInfo value in values)
            {
                yield return new PropertyRemoveChange(parent, value);
            }
        }

        private IEnumerable<IChange> CheckPropertyMatches(TypeInfo parent, IEnumerable<CompareMatch<PropertyInfo>> matches)
        {
            var changes = new List<IChange>();
            foreach (CompareMatch<PropertyInfo> match in matches)
            {
                MethodInfo oldGetter = match.OldValue.GetMethod;
                MethodInfo newGetter = match.NewValue.GetMethod;
                MethodInfo oldSetter = match.OldValue.SetMethod;
                MethodInfo newSetter = match.NewValue.SetMethod;

                CheckMethodVisibilityChange(oldGetter, newGetter, out bool getAdded, out bool getRemoved);
                CheckMethodVisibilityChange(oldSetter, newSetter, out bool setAdded, out bool setRemoved);

                if (getAdded) { changes.Add(new PropertyGetterAddChange(parent, match.NewValue)); }
                else if (getRemoved) { changes.Add(new PropertyGetterRemoveChange(parent, match.OldValue)); }

                if (setAdded) { changes.Add(new PropertySetterAddChange(parent, match.NewValue)); }
                else if (setRemoved) { changes.Add(new PropertySetterRemoveChange(parent, match.OldValue)); }

                if (!IsTypeEqual(match.OldValue.PropertyType, match.NewValue.PropertyType))
                {
                    changes.Add(new PropertyTypeChange(parent, match.OldValue, match.NewValue));
                }
            }

            return changes;
        }
    }
}
