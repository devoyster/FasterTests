using System.Collections.Generic;
using FasterTests.Core;
using FasterTests.Core.Interfaces;
using FasterTests.Core.Interfaces.Integration;
using FasterTests.Core.Interfaces.Models;
using Machine.Fakes;
using Machine.Specifications;

namespace FasterTests.Tests.Core
{
    [Subject(typeof(TestRunnerBootstrapper))]
    public class When_test_runner_bootstrapper_is_invoked : WithSubject<TestRunnerBootstrapper>
    {
        Because of = () =>
            Subject.Run();

        It should_inspect_tests_once = () => The<ITestInspector>().WasToldTo(i => i.LoadAllTestsFrom(Param.IsAny<string>())).OnlyOnce();

        It should_run_tests_once = () => The<ITestDispatcher>().WasToldTo(i => i.RunTests(Param.IsAny<IEnumerable<TestDescriptor>>())).OnlyOnce();

        It should_consume_tests_once = () => The<ITestResultsConsumer>().WasToldTo(i => i.Consume(Param.IsAny<IEnumerable<TestResult>>())).OnlyOnce();
    }
}