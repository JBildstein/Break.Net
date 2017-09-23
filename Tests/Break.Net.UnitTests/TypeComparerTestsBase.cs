namespace BreakDotNet.UnitTests
{
    public abstract class TypeComparerTestsBase
    {
        protected TypeComparer CreateComparer()
        {
            var settings = new TypeComparerSettings() { IgnoreCase = false };
            return new TypeComparer(settings);
        }
    }
}
