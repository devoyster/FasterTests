using FasterTests.Core.Integration.Nunit;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.TestEngineSpecs
{
    [Subject(typeof(TestEngine))]
    public class When_green_test_is_executed : TestEngineSpecification
    {
        Because of = () =>
            RunTest<PassingTest>();

        It should_pass = () => TestResult.IsSuccess.ShouldBeTrue();
    }
}