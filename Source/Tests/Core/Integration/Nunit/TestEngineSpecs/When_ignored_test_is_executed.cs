using FasterTests.Core.Integration.Nunit;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.TestEngineSpecs
{
    [Subject(typeof(TestEngine))]
    public class When_ignored_test_is_executed : TestEngineSpecification
    {
        Because of = () =>
            RunTest<IgnoredTest>();

        It should_be_ignored = () => TestResult.IsIgnored.ShouldBeTrue();
    }
}