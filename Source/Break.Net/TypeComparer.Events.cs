using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BreakDotNet.Changes;

namespace BreakDotNet
{
    public partial class TypeComparer
    {
        private IEnumerable<IChange> CheckEvents(CompareMatch<TypeInfo> match)
        {
            IEnumerable<EventInfo> oldValues = GetPublicEvents(match.OldValue.DeclaredEvents);
            IEnumerable<EventInfo> newValues = GetPublicEvents(match.NewValue.DeclaredEvents);
            CompareResult<EventInfo> compareResult = CompareEnumerables(oldValues, newValues, IsMemberEqual);

            return CheckEventAdditions(match.NewValue, compareResult.Added)
                .Concat(CheckEventRemovals(match.OldValue, compareResult.Removed))
                .Concat(CheckEventMatches(match.NewValue, compareResult.Matches));
        }

        private IEnumerable<EventInfo> GetPublicEvents(IEnumerable<EventInfo> events)
        {
            return events.Where(t => t.AddMethod?.IsPublic == true || t.RemoveMethod?.IsPublic == true);
        }

        private IEnumerable<IChange> CheckEventAdditions(TypeInfo parent, IEnumerable<EventInfo> values)
        {
            foreach (EventInfo value in values)
            {
                yield return new EventAddChange(parent, value);
            }
        }

        private IEnumerable<IChange> CheckEventRemovals(TypeInfo parent, IEnumerable<EventInfo> values)
        {
            foreach (EventInfo value in values)
            {
                yield return new EventRemoveChange(parent, value);
            }
        }

        private IEnumerable<IChange> CheckEventMatches(TypeInfo parent, IEnumerable<CompareMatch<EventInfo>> matches)
        {
            var changes = new List<IChange>();
            foreach (CompareMatch<EventInfo> match in matches)
            {
                MethodInfo oldAdd = match.OldValue.AddMethod;
                MethodInfo newAdd = match.NewValue.AddMethod;
                MethodInfo oldRemove = match.OldValue.RemoveMethod;
                MethodInfo newRemove = match.NewValue.RemoveMethod;

                CheckMethodVisibilityChange(oldAdd, newAdd, out bool addAdded, out bool addRemoved);
                CheckMethodVisibilityChange(oldRemove, newRemove, out bool removeAdded, out bool removeRemoved);

                if (addAdded) { changes.Add(new EventAdderAddChange(parent, match.NewValue)); }
                else if (addRemoved) { changes.Add(new EventAdderRemoveChange(parent, match.OldValue)); }

                if (removeAdded) { changes.Add(new EventRemoverAddChange(parent, match.NewValue)); }
                else if (removeRemoved) { changes.Add(new EventRemoverRemoveChange(parent, match.OldValue)); }

                if (!IsTypeEqual(match.OldValue.EventHandlerType, match.NewValue.EventHandlerType))
                {
                    changes.Add(new EventTypeChange(parent, match.OldValue, match.NewValue));
                }
            }

            return changes;
        }
    }
}
