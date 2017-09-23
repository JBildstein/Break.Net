namespace BreakDotNet.Changes
{
    public class PatchChange : IChange
    {
        public const string IdConstant = "TST003";

        public string Id
        {
            get { return IdConstant; }
        }
        public ChangeSeverity Severity
        {
            get { return ChangeSeverity.Patch; }
        }

        public string GetMessage()
        {
            return $"Patch change test";
        }
    }
}
