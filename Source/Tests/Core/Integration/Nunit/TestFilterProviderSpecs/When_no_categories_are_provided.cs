using FasterTests.Core.Integration.Nunit;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.TestFilterProviderSpecs
{
    [Subject(typeof(TestFilterProvider))]
    public class When_no_categories_are_provided : TestFilterProviderSpecification
    {
        Establish context = () =>
            Subject = new TestFilterProvider(null, null);

        Because of = () =>
            Result = Subject.TestFilter;

        It should_pass_for_any_test = () => Result.Pass(MockTestWithCategories()).ShouldBeTrue();
    }
}