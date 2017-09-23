using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using static System.Console;

namespace BreakDotNet
{
    public partial class Program
    {
        static int Main(string[] args)
        {
            try
            {
                CliParameters parameters = ArgumentParser.Parse(args);
                if (parameters.PrintHelp)
                {
                    PrintHeader();
                    PrintHelp();
                    return ExitCodes.Ok;
                }

                if (parameters.PrintVersion && (!parameters.HasOldAssemblyPath || !parameters.HasNewAssemblyPath))
                {
                    PrintVersion();
                    return ExitCodes.Ok;
                }

                if (parameters.InvalidArguments)
                {
                    PrintHelp("Invalid arguments");
                    return ExitCodes.InvalidArguments;
                }

                if (!parameters.HasOldAssemblyPath || !parameters.HasNewAssemblyPath)
                {
                    PrintHelp("No assembly paths given");
                    return ExitCodes.NoAssemblyPaths;
                }

                parameters.OldAssemblyPath = ValidatePath(parameters.OldAssemblyPath);
                if (!parameters.HasOldAssemblyPath) { return ExitCodes.AssemblyFileNotFound; }

                parameters.NewAssemblyPath = ValidatePath(parameters.NewAssemblyPath);
                if (!parameters.HasNewAssemblyPath) { return ExitCodes.AssemblyFileNotFound; }

                if (!parameters.Silent)
                {
                    PrintHeader();
                    PrintVersion();
                    WriteLine();
                }

                try
                {
                    Assembly oldAssembly = LoadAssembly(parameters.OldAssemblyPath);
                    Assembly newAssembly = LoadAssembly(parameters.NewAssemblyPath);

                    CompareFiles(oldAssembly, newAssembly, parameters);
                }
                catch (Exception ex) when (ex is PlatformNotSupportedException || ex is FileLoadException)
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Unable to load assemblies on this platform.");
                    WriteLine("Run the CLI with .Net Core for .Net Standard assemblies or with the full .Net framework otherwise.");
                    return ExitCodes.PlatformNotSupported;
                }
            }
            catch (Exception ex)
            {
                PrintError(ex);
                return ExitCodes.Exception;
            }
            
            return ExitCodes.Ok;
        }

        private static Assembly LoadAssembly(string path)
        {
#if NETCOREAPP2_0
            return new CustomLoadContext().LoadFromAssemblyPath(path);
#else
            return Assembly.ReflectionOnlyLoadFrom(path);
#endif
        }

        private static void CompareFiles(Assembly oldAssembly, Assembly newAssembly, CliParameters parameters)
        {
            var settings = new TypeComparerSettings()
            {
                IgnoreCase = parameters.IgnoreCase,
                AssemblyCaseSensitive = parameters.AssemblyCaseSensitive,
            };

            var comparer = new TypeComparer(settings);
            IEnumerable<IChange> changes = comparer.Compare(oldAssembly, newAssembly);

            if (parameters.PrintChange)
            {
                bool first = true;
                var groupedChanges = changes.OrderBy(t => t.Id).GroupBy(t => t.Severity);
                foreach (var group in groupedChanges)
                {
                    switch (group.Key)
                    {
                        case ChangeSeverity.Major:
                            ForegroundColor = ConsoleColor.Red;
                            break;
                        case ChangeSeverity.Minor:
                            ForegroundColor = ConsoleColor.Yellow;
                            break;
                        case ChangeSeverity.Patch:
                            ForegroundColor = ConsoleColor.Green;
                            break;
                    }

                    if (!first) { WriteLine(); }
                    WriteLine($"{group.Key} Changes:");
                    foreach (var change in group) { WriteLine($"  {change.Id}: {change.GetMessage()}"); }
                    first = false;
                }

                ForegroundColor = ConsoleColor.White;

                if (!groupedChanges.Any() && !parameters.Silent)
                {
                    WriteLine("No changes between assemblies");
                }
            }

            if (parameters.PrintRecommendation)
            {
                if (parameters.PrintChange) { WriteLine(); }

                if (parameters.Silent) { Write("Version: "); }
                else { Write("Recommended new version: "); }

                Version newVersion = TypeComparer.GetNewVersion(oldAssembly, changes);
                WriteLine(newVersion.ToString());
            }
        }

        private static string ValidatePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                PrintHelp("No path given");
                return null;
            }

            if (!Path.IsPathRooted(path))
            {
                string current = Directory.GetCurrentDirectory();
                path = Path.Combine(current, path);
            }

            if (!File.Exists(path))
            {
                PrintHelp($"File not found at: {path}");
                return null;
            }

            return path;
        }


        private static void PrintHeader()
        {
            WriteLine(@"  _____                _      _   _      _   ");
            WriteLine(@" | ___ \              | |    | \ | |    | |  ");
            WriteLine(@" | |_/ /_ __ ___  __ _| | __ |  \| | ___| |_ ");
            WriteLine(@" | ___ \ '__/ _ \/ _` | |/ / | . ` |/ _ \ __|");
            WriteLine(@" | |_/ / | |  __/ (_| |   < _| |\  |  __/ |_ ");
            WriteLine(@" \____/|_|  \___|\__,_|_|\_(_)_| \_/\___|\__|");
            WriteLine();
        }

        private static void PrintVersion()
        {
            var comparerAssembly = Assembly.GetAssembly(typeof(TypeComparer));
            var cliAssembly = Assembly.GetExecutingAssembly();

            WriteLine($"Library Version {comparerAssembly.GetName().Version}");
            WriteLine($"CLI Version {cliAssembly.GetName().Version}");
        }

        private static void PrintHelp(string error = null)
        {
            if (error != null)
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine($"Error: {error}");
                WriteLine();
                ForegroundColor = ConsoleColor.White;
            }

            WriteLine("Break.Net:");
            WriteLine("Compare .Net assemblies for changes according to semantic versioning rules");
            WriteLine();
            PrintVersion();
            WriteLine();
            WriteLine("Usage:");
            WriteLine("  breakdotnet-cli --old=<old-assembly> --new=<new-assembly> [options]");
            WriteLine("  breakdotnet-cli -h | --help");
            WriteLine("  breakdotnet-cli -v | --version");
            WriteLine();
            WriteLine("Options:");
            WriteLine("  -h --help                     Show this screen");
            WriteLine("  -v --version                  Show library and CLI version");
            WriteLine("  -s --silent                   Only print output without additional text");
            WriteLine("  -c --changes                  Show changes in the format \"ID: EXPLANATION\"");
            WriteLine("  -r --recommendation           Show recommended new assembly version");
            WriteLine("  --old                         Path to the old assembly");
            WriteLine("  --new                         Path to the new assembly");
            WriteLine("  --ignore-case                 Ignore case when comparing type or member names");
            WriteLine("  --assembly-case-sensitive     Comparison of assembly names is case sensitive");
        }

        private static void PrintError(Exception ex)
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine("Error while running Break.Net:");
            do
            {
                WriteLine($" -{ex.GetType().Name}: {ex.Message}");
                ex = ex.InnerException;
            } while (ex != null);

            ForegroundColor = ConsoleColor.White;
            WriteLine();
            WriteLine("Please file an issue on GitHub: https://github.com/JBildstein/Break.Net");
        }
    }
}
