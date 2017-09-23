namespace BreakDotNet.UnitTests.Models.Events
{
    public delegate void EventDelegate();

    public class AddChangeModel
    {
        public event EventDelegate Event;
    }
}
