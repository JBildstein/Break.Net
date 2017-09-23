namespace BreakDotNet.UnitTests.Models.Events
{
    public delegate void TypeChangeDelegateOld();

    public class TypeChangeModel
    {
        public event TypeChangeDelegateOld Event;
    }
}
