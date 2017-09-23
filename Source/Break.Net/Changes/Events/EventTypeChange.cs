using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Event type change
    /// </summary>
    public class EventTypeChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "EVE001";

        /// <summary>
        /// Change ID
        /// </summary>
        public string Id
        {
            get { return IdConstant; }
        }
        /// <summary>
        /// Severity of the change
        /// </summary>
        public ChangeSeverity Severity
        {
            get { return ChangeSeverity.Major; }
        }
        /// <summary>
        /// The type that contains the event
        /// </summary>
        public TypeInfo Parent { get; }
        /// <summary>
        /// The old event
        /// </summary>
        public EventInfo OldEvent { get; }
        /// <summary>
        /// The new event
        /// </summary>
        public EventInfo NewEvent { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="EventTypeChange"/> class
        /// </summary>
        /// <param name="parent">The type that contains the event</param>
        /// <param name="oldEvent">The old event</param>
        /// <param name="newEvent">The new event</param>
        public EventTypeChange(TypeInfo parent, EventInfo oldEvent, EventInfo newEvent)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            OldEvent = oldEvent ?? throw new ArgumentNullException(nameof(oldEvent));
            NewEvent = newEvent ?? throw new ArgumentNullException(nameof(newEvent));
        }

        /// <summary>
        /// Human readable message about the change
        /// </summary>
        /// <returns>The message about the change</returns>
        public string GetMessage()
        {
            return $"Event {NewEvent.Name} of type {Parent.FullName} changed type of delegate from " +
                $"{OldEvent.EventHandlerType.FullName} to {NewEvent.EventHandlerType.FullName}";
        }
    }
}
