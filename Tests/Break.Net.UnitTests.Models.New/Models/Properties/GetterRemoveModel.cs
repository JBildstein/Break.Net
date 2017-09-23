namespace BreakDotNet.UnitTests.Models.Properties
{
    public class GetterRemoveModel
    {
        public int Property
        {
            set { backingField = value; }
        }
        private int backingField;
    }
}
