namespace BreakDotNet.Changes
{
    public class MinorChange : IChange
    {
        public const string IdConstant = "TST002";

        public string Id
        {
            get { return IdConstant; }
        }
        public ChangeSeverity Severity
        {
            get { return ChangeSeverity.Minor; }
        }

        public string GetMessage()
        {
            return $"Minor change test";
        }
    }
}
