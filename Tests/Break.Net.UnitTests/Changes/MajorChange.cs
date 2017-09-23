namespace BreakDotNet.Changes
{
    public class MajorChange : IChange
    {
        public const string IdConstant = "TST001";

        public string Id
        {
            get { return IdConstant; }
        }
        public ChangeSeverity Severity
        {
            get { return ChangeSeverity.Major; }
        }
        
        public string GetMessage()
        {
            return $"Major change test";
        }
    }
}
