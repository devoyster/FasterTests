using FasterTests.Core.Implementation.Integration.Nunit;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.NunitTestEngineSpecs
{
    [Subject(typeof(NunitTestEngine))]
    public class When_red_nunit_test_is_executed : NunitTestEngineSpecification
    {
        Because of = () =>
            RunTest<TestWithFailingAssert>();

        It should_fail = () => TestResult.IsSuccess.ShouldBeFalse();
    }
}