using FasterTests.Core.Implementation.Integration.Nunit;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.TestEngineSpecs
{
    [Subject(typeof(TestEngine))]
    public class When_test_which_throws_an_exception_is_executed : NunitTestEngineSpecification
    {
        Because of = () =>
            RunTest<TestWhichThrowsAnException>();

        It should_fail = () => TestResult.IsSuccess.ShouldBeFalse();

        It should_fill_error_message = () => TestResult.ErrorMessage.ShouldNotBeEmpty();
    }
}