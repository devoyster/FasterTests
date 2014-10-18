using FasterTests.Core.Integration.Nunit;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.TestFilterProviderSpecs
{
    [Subject(typeof(TestFilterProvider))]
    public class When_exclude_categories_are_provided : TestFilterProviderSpecification
    {
        Establish context = () =>
            Subject = new TestFilterProvider(null, new[] { "1", "2" });

        Because of = () =>
            Result = Subject.TestFilter;

        It should_not_pass_for_test_with_excluded_category = () => Result.Pass(MockTestWithCategories("2", "3")).ShouldBeFalse();

        It should_pass_for_test_without_excluded_category = () => Result.Pass(MockTestWithCategories("3")).ShouldBeTrue();

        It should_pass_for_test_without_categories = () => Result.Pass(MockTestWithCategories()).ShouldBeTrue();
    }
}