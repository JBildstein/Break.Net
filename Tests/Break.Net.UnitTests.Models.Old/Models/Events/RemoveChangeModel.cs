namespace BreakDotNet.UnitTests.Models.Events
{
    public delegate void EventDelegate();

    public class RemoveChangeModel
    {
        public event EventDelegate Event;
    }
}
