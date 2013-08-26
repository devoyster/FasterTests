using FasterTests.Core.Integration.Nunit;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.TestEngineSpecs
{
    [Subject(typeof(TestEngine))]
    public class When_explicit_test_is_executed : TestEngineSpecification
    {
        Because of = () =>
            RunTest<ExplicitTest>();

        It should_not_run = () => TestResult.ShouldBeNull();
    }
}