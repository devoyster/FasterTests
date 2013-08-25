using FasterTests.Core.Integration.Nunit;
using FasterTests.Tests.NunitTestAssembly.FailingNamespace;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.TestEngineSpecs
{
    [Subject(typeof(TestEngine))]
    public class When_test_with_failing_setup_fixture_is_executed : TestEngineSpecification
    {
        Establish context = () =>
            WhenToldToSetupForReturn(false);

        Because of = () =>
            RunTest<TestWithFailingSetupFixture>();

        It should_fail = () => TestResult.IsSuccess.ShouldBeFalse();
    }
}