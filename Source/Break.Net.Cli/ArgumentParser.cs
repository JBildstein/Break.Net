using System;

namespace BreakDotNet
{
    public static class ArgumentParser
    {
        public const string OldAssemblyPath = "--old";
        public const string NewAssemblyPath = "--new";
        
        public const string IgnoreCase = "--ignore-case";
        public const string AssemblyCaseSensitive = "--assembly-case-sensitive";

        public const string Silent = "--silent";
        public const string SilentShort = "-s";

        public const string PrintChange = "--changes";
        public const string PrintChangeShort = "-c";

        public const string PrintRecommendation = "--recommendation";
        public const string PrintRecommendationShort = "-r";

        public const string PrintVersion = "--version";
        public const string PrintVersionShort = "-v";

        public const string PrintHelp = "--help";
        public const string PrintHelpShort = "-h";

        public static CliParameters Parse(string[] args)
        {
            var parameters = new CliParameters();
            foreach (string arg in args)
            {
                if (arg.IsParameter(OldAssemblyPath))
                {
                    parameters.OldAssemblyPath = ParsePath(arg, OldAssemblyPath);
                }
                else if (arg.IsParameter(NewAssemblyPath))
                {
                    parameters.NewAssemblyPath = ParsePath(arg, NewAssemblyPath);
                }
                else if (arg.IsParameterExact(IgnoreCase))
                {
                    parameters.IgnoreCase = true;
                }
                else if (arg.IsParameterExact(AssemblyCaseSensitive))
                {
                    parameters.AssemblyCaseSensitive = true;
                }
                else if (arg.IsParameterExact(Silent) || arg.IsParameterExact(SilentShort))
                {
                    parameters.Silent = true;
                }
                else if (arg.IsParameterExact(PrintChange) || arg.IsParameterExact(PrintChangeShort))
                {
                    parameters.PrintChange = true;
                }
                else if (arg.IsParameterExact(PrintRecommendation) || arg.IsParameterExact(PrintRecommendationShort))
                {
                    parameters.PrintRecommendation = true;
                }
                else if (arg.IsParameterExact(PrintVersion) || arg.IsParameterExact(PrintVersionShort))
                {
                    parameters.PrintVersion = true;
                }
                else if (arg.IsParameterExact(PrintHelp) || arg.IsParameterExact(PrintHelpShort))
                {
                    parameters.PrintHelp = true;
                    break;
                }
                else
                {
                    parameters.InvalidArguments = true;
                    break;
                }
            }

            return parameters;
        }

        private static bool IsParameter(this string arg, string id)
        {
            return arg.StartsWith(id, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsParameterExact(this string arg, string id)
        {
            return arg.Equals(id, StringComparison.OrdinalIgnoreCase);
        }

        private static string ParsePath(string arg, string id)
        {
            int idLength = id.Length;
            return arg.Substring(idLength).TrimStart('=').Trim('"');
        }
    }
}
