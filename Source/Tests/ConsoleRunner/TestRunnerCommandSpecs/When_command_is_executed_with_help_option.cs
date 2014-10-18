using FasterTests.ConsoleRunner;
using FasterTests.Core.Interfaces;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.ConsoleRunner.TestRunnerCommandSpecs
{
    [Subject(typeof(TestRunnerCommand))]
    public class When_command_is_executed_with_help_option : TestRunnerCommandSpecification
    {
        Because of =
            () => Subject.Execute(new[] { "Test.Assembly.dll", "-h" }, Output);

        It should_not_run_tests = () => The<ITestRunner>().WasNotToldTo(r => r.Run(Param.IsAny<TestRunSettings>()));

        It should_write_help = () => Output.ToString().ShouldContain("Usage: FasterTests-Run \"test assembly path\" [options]");
    }
}