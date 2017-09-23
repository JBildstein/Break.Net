using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Removed add accessor for event change
    /// </summary>
    public class EventAdderRemoveChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "EVE005";

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
        /// The changed event
        /// </summary>
        public EventInfo Event { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="EventAdderRemoveChange"/> class
        /// </summary>
        /// <param name="parent">The type that contains the event</param>
        /// <param name="eventInfo">The changed event</param>
        public EventAdderRemoveChange(TypeInfo parent, EventInfo eventInfo)
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            Event = eventInfo ?? throw new ArgumentNullException(nameof(eventInfo));
        }

        /// <summary>
        /// Human readable message about the change
        /// </summary>
        /// <returns>The message about the change</returns>
        public string GetMessage()
        {
            return $"Add method for event {Event.Name} of type {Parent.FullName} got removed";
        }
    }
}
