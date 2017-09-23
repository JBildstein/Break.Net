namespace BreakDotNet.UnitTests.Models.Methods
{
    public class ParameterMetaOutChangeModel
    {
        public void Method(out int parameter)
        {
            parameter = 0;
        }
    }

    public class ParameterMetaRefChangeModel
    {
        public void Method(ref int parameter)
        { }
    }

    public class ParameterMetaDefaultChangeModel
    {
        public void Method(int parameter = 0)
        { }
    }
}
