using FasterTests.Core.Implementation.Integration.Nunit;
using FasterTests.Tests.NunitTestAssembly.AnotherNamespace;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.NunitTestEngineSpecs
{
    [Subject(typeof(NunitTestEngine))]
    public class When_test_with_setup_fixture_is_executed_twice : NunitTestEngineSpecification
    {
        Establish context = () =>
            AnotherNamespaceSetupFixture.InvocationCount = 0;

        Because of = () =>
            RunTests(typeof(AnotherTestWithSetupFixture), typeof(AnotherTestWithSetupFixture));

        It should_invoke_setup_fixture_only_once = () => AnotherNamespaceSetupFixture.InvocationCount.ShouldEqual(1);
    }
}