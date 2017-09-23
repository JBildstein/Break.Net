namespace BreakDotNet.UnitTests.Models.Constructors
{
    public class ParameterMetaOutChangeModel
    {
        public ParameterMetaOutChangeModel(out int parameter)
        {
            parameter = 0;
        }
    }

    public class ParameterMetaRefChangeModel
    {
        public ParameterMetaRefChangeModel(ref int parameter)
        { }
    }

    public class ParameterMetaDefaultChangeModel
    {
        public ParameterMetaDefaultChangeModel(int parameter = 0)
        { }
    }
}
