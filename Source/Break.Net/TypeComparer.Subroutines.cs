using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BreakDotNet
{
    public partial class TypeComparer
    {
        private void CheckMethodVisibilityChange(MethodInfo oldMethod, MethodInfo newMethod, out bool added, out bool removed)
        {
            added = removed = false;
            if (oldMethod != null)
            {
                if (oldMethod.IsPublic) { removed = newMethod == null || (newMethod != null && !newMethod.IsPublic); }
                else { added = newMethod != null && newMethod.IsPublic; }
            }
            else { added = newMethod != null && newMethod.IsPublic; }
        }

        private CompareResult<T> CompareEnumerables<T>(IEnumerable<T> olds, IEnumerable<T> news, Func<T, T, bool> comparer)
            where T : class
        {
            List<T> oldValues = olds.ToList();
            var matches = new List<CompareMatch<T>>();
            var added = new List<T>();
            foreach (T newValue in news)
            {
                T oldValue = oldValues.FirstOrDefault(t => comparer(t, newValue));
                if (oldValue != null)
                {
                    oldValues.Remove(oldValue);
                    matches.Add(new CompareMatch<T>(oldValue, newValue));
                }
                else { added.Add(newValue); }
            }

            return new CompareResult<T>
            {
                Added = added,
                Removed = oldValues,
                Matches = matches,
            };
        }

        private bool IsTypeInfoEqual(TypeInfo oldValue, TypeInfo newValue)
        {
            return IsTypeOrMemberNameEqual(oldValue.FullName, newValue.FullName);
        }

        private bool IsTypeEqual(Type oldValue, Type newValue)
        {
            return IsTypeOrMemberNameEqual(oldValue.FullName, newValue.FullName);
        }

        private bool IsMemberEqual(MemberInfo oldValue, MemberInfo newValue)
        {
            return IsTypeOrMemberNameEqual(oldValue.Name, newValue.Name);
        }

        private bool IsMethodParameterCountEqual(MethodBase oldValue, MethodBase newValue)
        {
            return oldValue.GetParameters().Length == newValue.GetParameters().Length;
        }

        private bool IsMethodParameterTypesEqual(MethodBase oldValue, MethodBase newValue)
        {
            ParameterInfo[] oldParams = oldValue.GetParameters();
            ParameterInfo[] newParams = newValue.GetParameters();

            for (int i = 0; i < oldParams.Length; i++)
            {
                if (HasParameterTypeChanged(oldParams[i].ParameterType, newParams[i].ParameterType)) { return false; }
            }

            return true;
        }

        private bool IsTypeOrMemberNameEqual(string oldName, string newName)
        {
            return IsNameEqual(oldName, newName, Settings.IgnoreCase);
        }

        private bool IsNameEqual(string oldName, string newName, bool ignoreCase)
        {
            StringComparison comparer = StringComparison.Ordinal;
            if (ignoreCase) { comparer = StringComparison.OrdinalIgnoreCase; }

            return string.Equals(oldName, newName, comparer);
        }


        private bool HasParameterTypeChanged(ParameterInfo oldParameter, ParameterInfo newParameter)
        {
            return HasParameterTypeChanged(oldParameter.ParameterType, newParameter.ParameterType);
        }

        private bool HasParameterTypeChanged(Type oldParameter, Type newParameter)
        {
            // Generic parameter has no FullName
            string oldName = oldParameter.FullName ?? oldParameter.Name;
            string newName = newParameter.FullName ?? newParameter.Name;

            return !IsTypeOrMemberNameEqual(oldName?.Trim('&'), newName?.Trim('&'));
        }

        private bool HasParameterNameChanged(ParameterInfo oldParameter, ParameterInfo newParameter)
        {
            return !IsTypeOrMemberNameEqual(oldParameter.Name, newParameter.Name);
        }

        private bool HasParameterMetaChanged(ParameterInfo oldParameter, ParameterInfo newParameter)
        {
            return oldParameter.IsIn != newParameter.IsIn ||
                oldParameter.IsOut != newParameter.IsOut ||
                oldParameter.ParameterType.IsByRef != newParameter.ParameterType.IsByRef ||
                oldParameter.IsRetval != newParameter.IsRetval ||
                IsParameterOptional(oldParameter) != IsParameterOptional(newParameter);
        }

        private bool HasParameterDefaultValueChanged(ParameterInfo oldParameter, ParameterInfo newParameter)
        {
            // TODO: comparison can return false positives when non-primitive values are used
            return IsParameterOptional(oldParameter) && IsParameterOptional(newParameter) &&
                  !Equals(oldParameter.DefaultValue, newParameter.DefaultValue);
        }

        private bool IsParameterOptional(ParameterInfo parameter)
        {
            // HasDefaultValue is more reliable but can't be accessed from assemblies loaded in reflection-only context
            // If IsOptional is set correctly is up to the compiler and might not be set
            try
            {
                if (!IsReflectionOnly) { return parameter.HasDefaultValue; }
            }
            catch (InvalidOperationException) { IsReflectionOnly = true; }

            return parameter.IsOptional;
        }

        private object GetParameterDefaultValue(ParameterInfo parameter)
        {
            // DefaultValue can't be accessed from assemblies loaded in reflection-only context
            // RawDefaultValue can be accessed from either context but is not available until netstandard1.5
#if NETSTANDARD1_5
            return parameter.RawDefaultValue;
#else
            try
            {
                if (!IsReflectionOnly) { return parameter.DefaultValue; }
            }
            catch (InvalidOperationException) { IsReflectionOnly = true; }

            return null;
#endif
        }
    }
}
