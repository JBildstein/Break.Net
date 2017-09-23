namespace BreakDotNet.UnitTests.Models.Properties
{
    public class GetterAddModel
    {
        public int Property
        {
            set { backingField = value; }
        }
        private int backingField;
    }
}
