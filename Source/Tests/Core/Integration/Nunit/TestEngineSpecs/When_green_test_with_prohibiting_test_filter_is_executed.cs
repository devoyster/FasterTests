using FasterTests.Core.Integration.Nunit;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Specifications;
using NUnit.Core.Filters;

namespace FasterTests.Tests.Core.Integration.Nunit.TestEngineSpecs
{
    [Subject(typeof(TestEngine))]
    public class When_green_test_with_prohibiting_test_filter_is_executed : TestEngineSpecification
    {
        Establish context = () =>
            WhenToldToTestFilterReturn(new SimpleNameFilter("other test name"));

        Because of = () =>
            RunTest<PassingTest>();

        It should_not_run = () => TestResult.ShouldBeNull();
    }
}