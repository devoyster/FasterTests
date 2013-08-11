using FasterTests.Core.Implementation.Integration.Nunit;
using FasterTests.Tests.NunitTestAssembly.FailingNamespace;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.NunitTestEngineSpecs
{
    [Subject(typeof(NunitTestEngine))]
    public class When_test_with_failing_setup_fixture_is_executed : NunitTestEngineSpecification
    {
        Because of = () =>
            RunTest<TestWithFailingSetupFixture>();

        It should_fail = () => TestResult.IsSuccess.ShouldBeFalse();
    }
}