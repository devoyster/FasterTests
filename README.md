# FasterTests - parallel unit test runner

FasterTests is a console unit tests runner which can execute your regular tests in parallel to run them faster. And you won't need to change your existing test code for that.

You can [download FasterTests from GitHub](../../releases) or install it from NuGet:

```
Install-Package FasterTests
```

And then run it from console it as pretty much any other test runner:

```bat
FasterTests-Run "My.NUnit.Tests.Assembly.dll" -ExcludeCategories "Slow,Full"
```

This will run all tests from your test assembly except those marked with "Slow" and "Full" categories. Tests will be running in several parallel threads, each thread with its own AppDomain.

Runner behavior can be adjusted with optional command-line arguments:

* `-IncludeCategories` - comma-separated list of test categories to include into run
* `-ExcludeCategories` - comma-separated list of test categories to exclude from run
* `-DegreeOfParallelism` - parallel threads count. Defaults to machine logical processors (usually cores) count
* `-Help` - shows help about command-line arguments


## Handling conflicting tests

Some tests cannot be run in parallel due to conflicts - common example is DB tests. FasterTests provides two workarounds to solve this problem:

### Group tests which can't run in parallel

You can group together tests which cannot run in parallel using `-NoParallelGroups` argument. Example:

```bat
FasterTests-Run "My.NUnit.Tests.Assembly.dll" -NoParallelGroups "Tests.Database.;Tests.Elastic.,Tests.Solr."
```

In this example test classes which full name starts with "Tests.Database." will not run in parallel with other such tests. Same is true for the second group - tests which have names starting with "Tests.Elastic." or "Tests.Solr." will not run in parallel with other tests in the same namespaces.

### Patch test config per thread

You can tell FasterTests to patch certain strings in test assembly .config file before running test in each of the threads using `-ConfigStringsToPatch` argument. Example:

```bat
FasterTests-Run "My.NUnit.Tests.Assembly.dll" -ConfigStringsToPatch "=test_db,=other_test_db"
```

Here FasterTests will patch tests .config for each thread by searching for provided substrings (comma-separated) and adding thread index numeric suffix to them (for all threads except the first one). So if 3 threads are running tests in parallel then substring "=test_db" will be replaced with "=test_db_2" for thread #2 and "=test_db_3" for thread #3, and the same will be true for "=other_test_db".

This is useful for storage tests (like DB tests) to make sure that each test thread uses its own storage.

## Known limitations

Work on FasterTests is in progress so it has some limitations (a.k.a. project TODO list):

* Works for NUnit tests only
* Cannot output results in NUnit XML or Team—ity formats
* Always creates threads in the same process, with dedicated AppDomains
* Cannot process more than one test assembly
* Cannot run specific test - you can only provide categories to include/exclude
* No ReSharper and/or VS plugin

## Other runners

There are other free parallel NUnit test runners out there. Some of them are more mature and feature-rich, but FasterTests seems to be faster :) If FasterTests won't fit your requirements then maybe other runners will.

Here are links to some other parallel runners I've discovered:

* https://github.com/nunit/dev/wiki/Framework-Parallel-Test-Execution
* https://cpntr.codeplex.com/
* https://nunitmulticore.codeplex.com/
* http://www.codeproject.com/Tips/134389/Running-NUnit-tests-in-parallel
* https://github.com/Ancestry/ParallelizeNunit
* https://github.com/faisalmansoor/ParallelNUnit
* https://github.com/opentable/nunit-parallel-runner
* https://github.com/fourth/TestRunner