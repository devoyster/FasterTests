using FasterTests.Core.Implementation.Integration.Nunit;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.TestEngineSpecs
{
    [Subject(typeof(TestEngine))]
    public class When_ignored_nunit_test_is_executed : NunitTestEngineSpecification
    {
        Because of = () =>
            RunTest<IgnoredTest>();

        It should_be_ignored = () => TestResult.IsIgnored.ShouldBeTrue();
    }
}