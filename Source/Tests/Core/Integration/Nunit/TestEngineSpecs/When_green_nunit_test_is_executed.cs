using FasterTests.Core.Integration.Nunit;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.TestEngineSpecs
{
    [Subject(typeof(TestEngine))]
    public class When_green_nunit_test_is_executed : NunitTestEngineSpecification
    {
        Because of = () =>
            RunTest<PassingTest>();

        It should_pass = () => TestResult.IsSuccess.ShouldBeTrue();
    }
}