using FasterTests.ConsoleRunner;
using FasterTests.Core.Interfaces;
using Machine.Specifications;
using Machine.Fakes;
using System.Linq;

namespace FasterTests.Tests.ConsoleRunner.TestRunnerCommandSpecs
{
    [Subject(typeof(TestRunnerCommand))]
    public class When_command_is_executed_with_groups_options : TestRunnerCommandSpecification
    {
        Because of =
            () => Subject.Execute(new[] { "Test.Assembly.dll", "-g", "1,2;3,4,5" }, Output);

        It should_supply_groups_to_test_run =
            () => The<ITestRunner>().WasToldTo(r => r.Run(Param<TestRunSettings>.Matches(s => s.NoParallelGroups[0].SequenceEqual(new[] { "1", "2" })
                                                                                              && s.NoParallelGroups[1].SequenceEqual(new[] { "3", "4", "5" })))).OnlyOnce();
    }
}