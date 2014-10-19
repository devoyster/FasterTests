using System.Linq;
using FasterTests.ConsoleRunner;
using FasterTests.Core.Interfaces;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.ConsoleRunner.TestRunnerCommandSpecs
{
    [Subject(typeof(TestRunnerCommand))]
    public class When_command_is_executed_without_arguments : TestRunnerCommandSpecification
    {
        Because of =
            () => Subject.Execute(Enumerable.Empty<string>(), Output);

        It should_not_run_tests = () => The<ITestRunner>().WasNotToldTo(r => r.Run(Param.IsAny<TestRunSettings>()));

        It should_write_error_message = () => Output.ToString().ShouldStartWith("Error: Test assembly path should be provided as first argument");

        It should_write_help = () => Output.ToString().ShouldContain("Usage: FasterTests-Run [testassemblies] [options]");
    }
}