using FasterTests.Core.Integration.Nunit;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.TestFilterProviderSpecs
{
    [Subject(typeof(TestFilterProvider))]
    public class When_include_and_exclude_categories_are_provided : TestFilterProviderSpecification
    {
        Establish context = () =>
            Subject = new TestFilterProvider(new[] { "1", "2" }, new[] { "3", "4", "5" });

        Because of = () =>
            Result = Subject.TestFilter;

        It should_pass_for_test_with_included_category = () => Result.Pass(MockTestWithCategories("2")).ShouldBeTrue();

        It should_not_pass_for_test_with_included_category_which_is_also_excluded = () => Result.Pass(MockTestWithCategories("3")).ShouldBeFalse();

        It should_not_pass_for_test_without_included_category = () => Result.Pass(MockTestWithCategories("6")).ShouldBeFalse();

        It should_not_pass_for_test_without_categories = () => Result.Pass(MockTestWithCategories()).ShouldBeFalse();
    }
}