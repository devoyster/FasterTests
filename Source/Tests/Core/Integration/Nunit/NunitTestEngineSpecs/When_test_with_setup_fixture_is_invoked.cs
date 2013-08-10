using FasterTests.Core.Implementation.Integration.Nunit;
using FasterTests.Tests.NunitTestAssembly.Namespace;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.NunitTestEngineSpecs
{
    [Subject(typeof(NunitTestEngine))]
    public class When_test_with_setup_fixture_is_executed : NunitTestEngineSpecification
    {
        Establish context = () =>
            SetUpFixture.WasInvoked = false;

        Because of = () =>
            RunTest<TestWithSetUpFixture>();

        It should_invoke_setup_fixture = () => SetUpFixture.WasInvoked.ShouldBeTrue();
    }
}