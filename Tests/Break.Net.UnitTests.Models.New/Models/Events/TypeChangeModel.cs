namespace BreakDotNet.UnitTests.Models.Events
{
    public delegate void TypeChangeDelegateNew();

    public class TypeChangeModel
    {
        public event TypeChangeDelegateNew Event;
    }
}
