using FasterTests.Core.Implementation.Integration.Nunit;
using FasterTests.Tests.NunitTestAssembly;
using FasterTests.Tests.NunitTestAssembly.AnotherNamespace;
using FasterTests.Tests.NunitTestAssembly.Namespace;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.NunitTestEngineSpecs
{
    [Subject(typeof(NunitTestEngine))]
    public class When_test_with_setup_fixtures_is_executed : NunitTestEngineSpecification
    {
        Establish context = () =>
        {
            RootSetupFixture.WasInvoked = false;
            NamespaceSetupFixture.WasInvoked = false;
            AnotherNamespaceSetupFixture.InvocationCount = 0;
        };

        Because of = () =>
            RunTest<TestWithSetupFixture>();

        It should_invoke_root_setup_fixture = () => RootSetupFixture.WasInvoked.ShouldBeTrue();

        It should_invoke_namespace_setup_fixture = () => NamespaceSetupFixture.WasInvoked.ShouldBeTrue();

        It should_not_invoke_another_namespace_setup_fixture = () => AnotherNamespaceSetupFixture.InvocationCount.ShouldEqual(0);
    }
}