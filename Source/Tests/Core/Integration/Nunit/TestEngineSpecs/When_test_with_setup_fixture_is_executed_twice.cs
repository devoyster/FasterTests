using FasterTests.Core.Integration.Nunit;
using FasterTests.Core.Integration.Nunit.SetupFixtures;
using FasterTests.Tests.NunitTestAssembly.AnotherNamespace;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.TestEngineSpecs
{
    [Subject(typeof(TestEngine))]
    public class When_test_with_setup_fixture_is_executed_twice : NunitTestEngineSpecification
    {
        Establish context = () =>
        {
            AnotherNamespaceSetupFixture.InvocationCount = 0;

            Configure(r => r.For<string>().Use(typeof(AnotherNamespaceSetupFixture).Assembly.Location));
            Configure<ISetupFixtureFactory, SetupFixtureFactory>();
            Configure<ISetupFixturesContext, AssemblySetupFixturesContext>();
        };

        Because of = () =>
            RunTests(typeof(AnotherTestWithSetupFixture), typeof(AnotherTestWithSetupFixture));

        It should_invoke_setup_fixture_only_once = () => AnotherNamespaceSetupFixture.InvocationCount.ShouldEqual(1);
    }
}