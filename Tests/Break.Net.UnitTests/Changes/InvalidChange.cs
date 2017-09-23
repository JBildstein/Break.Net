namespace BreakDotNet.Changes
{
    public class InvalidChange : IChange
    {
        public const string IdConstant = "TST004";

        public string Id
        {
            get { return IdConstant; }
        }
        public ChangeSeverity Severity
        {
            get { return (ChangeSeverity)(-1000); }
        }

        public string GetMessage()
        {
            return $"Invalid change test";
        }
    }
}
