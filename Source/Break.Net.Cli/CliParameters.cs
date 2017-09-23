namespace BreakDotNet
{
    public class CliParameters
    {
        public string OldAssemblyPath { get; set; }
        public string NewAssemblyPath { get; set; }
        public bool IgnoreCase { get; set; }
        public bool AssemblyCaseSensitive { get; set; }

        public bool Silent { get; set; }
        public bool PrintChange { get; set; }
        public bool PrintRecommendation { get; set; }
        public bool PrintVersion { get; set; }
        public bool PrintHelp { get; set; }

        public bool InvalidArguments { get; set; }

        public bool HasOldAssemblyPath
        {
            get { return !string.IsNullOrWhiteSpace(OldAssemblyPath); }
        }
        public bool HasNewAssemblyPath
        {
            get { return !string.IsNullOrWhiteSpace(NewAssemblyPath); }
        }
    }
}
