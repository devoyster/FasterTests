using System;
using FasterTests.ConsoleRunner;
using FasterTests.Core.Interfaces;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.ConsoleRunner.TestRunnerCommandSpecs
{
    [Subject(typeof(TestRunnerCommand))]
    public class When_command_is_executed_without_options : TestRunnerCommandSpecification
    {
        Because of =
            () => Subject.Execute(new[] { "Test.Assembly.dll" }, Output);

        It should_run_tests = () => The<ITestRunner>().WasToldTo(r => r.Run(Param.IsAny<TestRunSettings>())).OnlyOnce();

        It should_supply_assembly_path_to_test_run =
            () => The<ITestRunner>().WasToldTo(r => r.Run(Param<TestRunSettings>.Matches(s => s.AssemblyPath == "Test.Assembly.dll"))).OnlyOnce();

        It should_supply_output_to_test_run =
            () => The<ITestRunner>().WasToldTo(r => r.Run(Param<TestRunSettings>.Matches(s => s.Output == Output))).OnlyOnce();

        It should_use_machine_processors_count_as_degree_of_parallelism =
            () => The<ITestRunner>().WasToldTo(r => r.Run(Param<TestRunSettings>.Matches(s => s.DegreeOfParallelism == Environment.ProcessorCount))).OnlyOnce();
    }
}