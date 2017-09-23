using System;
using System.Reflection;

namespace BreakDotNet.Changes
{
    /// <summary>
    /// Added add accessor for event change
    /// </summary>
    public class EventAdderAddChange : IChange
    {
        /// <summary>
        /// Change ID
        /// </summary>
        public const string IdConstant = "EVE006";

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
            get { return ChangeSeverity.Minor; }
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
        /// Creates a new instance of the <see cref="EventAdderAddChange"/> class
        /// </summary>
        /// <param name="parent">The type that contains the event</param>
        /// <param name="eventInfo">The changed event</param>
        public EventAdderAddChange(TypeInfo parent, EventInfo eventInfo)
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
            return $"New add method for event {Event.Name} of type {Parent.FullName} added";
        }
    }
}
