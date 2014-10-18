using FasterTests.ConsoleRunner;
using FasterTests.Core.Interfaces;
using Machine.Specifications;
using Machine.Fakes;
using System.Linq;

namespace FasterTests.Tests.ConsoleRunner.TestRunnerCommandSpecs
{
    [Subject(typeof(TestRunnerCommand))]
    public class When_command_is_executed_with_exclude_option : TestRunnerCommandSpecification
    {
        Because of =
            () => Subject.Execute(new[] { "Test.Assembly.dll", "-x", "1,2,3" }, Output);

        It should_supply_categories_to_exclude_to_test_run =
            () => The<ITestRunner>().WasToldTo(r => r.Run(Param<TestRunSettings>.Matches(s => s.ExcludeCategories.SequenceEqual(new[] { "1", "2", "3" })))).OnlyOnce();
    }
}