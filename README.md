# Break.Net
Compare .Net assemblies for changes according to semantic versioning rules.

## Status

This project is still in an alpha state because it's very new and doesn't handle enough cases.
Currently only public types and members are compared and there is very little handling for generics.
These two things are the main points on the roadmap as well as handling some edge cases.

A list of currently supported changes can be found here:
[List of detectable changes](DetectableChanges.md)

## Library

The public API is pretty simple and basically is just the `TypeComparer` class.
It provides several `Compare` methods taking two assemblies or two lists of `Type` or `TypeInfo`.
All of them return a list of changes as `IEnumerable<IChange>`.
A change has an ID, a severity (Major, Minor, Patch) and a message explaining the change.

With the `TypeComparerSettings` you can influence how the comparison works. The following options are available:

| Setting | Description | Default |
| :-- | -- | -- |
| IgnoreCase | When true, comparison of type and member names is case insensitive | False |
| AssemblyCaseSensitive | When true, comparison of assembly names is case sensitive | False |

Additionally, there is the static method `GetNewVersion` which takes the changes and either an assembly or a version number.
It checks the changes and returns an updated version number depending on the highest change severity.

Depending on the used .Net Standard version, some checks are different or not available:

| Version | Remarks |
| :-- | -- |
| 1.0 | Baseline |
| 1.5 | - References are checked if the `Compare(Assembly, Assembly)` overload is used <br />- Checking a parameter default value works in a reflection-only context |
| 2.0 | If the `Compare(Assembly, Assembly)` overload is used, a check for reflection-only is done instead of running into an exception |

## CLI

With the CLI you can compare assemblies simply by providing two file paths. If you use the `--silent` switch the result can be automatically parsed.

**Important: Depending on the assembly framework target you also need to run the CLI with the same one.
i.e. you need to run it with .Net Core for .Net Standard assemblies and with the full .Net framework for assemblies targeting the full framework.**

### Usage:
```
breakdotnet-cli --old=<old-assembly> --new=<new-assembly> [options]
breakdotnet-cli -h | --help
breakdotnet-cli -v | --version
```

### Options:

| Switch | Description |
| :-- | -- |
| -h --help | Shows the help |
| -v --version | Shows the library and CLI version |
| -s --silent | Only print output without any additional text |
| --c --changes | Show changes in the format "ID: EXPLANATION" |
| -r --recommendation | Show recommended new assembly version |
| --old | Path to the old assembly |
| --new | Path to the new assembly |
| --ignore-case | Ignore case when comparing type or member names |
| --assembly-case-sensitive | Comparison of assembly names is case sensitive |

### Exit Codes:

| Code | Description |
| :-- | -- |
| 0 | Ok |
| 1 | An unexpected Exception happened |
| 2 | An invalid argument was provided |
| 3 | No or only one assembly path was provided |
| 4 | One of the assembly files was not found |
| 5 | Platform not supported. Make sure to run the correct framework (.Net Core or full .Net) |

### Examples:

List changes and show the new recommended version:
```
breakdotnet-cli --old="Path\To\OldAssembly.dll" --new="Path\To\NewAssembly.dll" -c -r
```

List only changes and nothing else:
```
breakdotnet-cli --old="Path\To\OldAssembly.dll" --new="Path\To\NewAssembly.dll" -c -s
```

Only print out new recommended version:
```
breakdotnet-cli --old="Path\To\OldAssembly.dll" --new="Path\To\NewAssembly.dll" -r -s
```

List changes and ignore case when comparing type or member names:
```
breakdotnet-cli --old="Path\To\OldAssembly.dll" --new="Path\To\NewAssembly.dll" -c --ignore-case
```

## Contributions

Any contribution is welcome! File an [issue](https://github.com/JBildstein/Break.Net/issues) if you have a problem
or create a [pull request](https://github.com/JBildstein/Break.Net/pulls) if you fixed a bug or added a new feature.
