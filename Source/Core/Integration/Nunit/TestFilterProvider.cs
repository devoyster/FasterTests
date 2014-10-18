using System.Linq;
using NUnit.Core;
using NUnit.Core.Filters;

namespace FasterTests.Core.Integration.Nunit
{
    public class TestFilterProvider : ITestFilterProvider
    {
        public TestFilterProvider(string[] includeCategories, string[] excludeCategories)
        {
            var includeCategoryFilter = CreateCategoryFilter(includeCategories);

            var excludeCategoryFilter = CreateCategoryFilter(excludeCategories);
            if (excludeCategoryFilter != null)
            {
                excludeCategoryFilter = new NotFilter(excludeCategoryFilter, topLevel: true);
            }

            var topFilter = includeCategoryFilter;
            if (topFilter == null)
            {
                topFilter = excludeCategoryFilter;
            }
            else if (excludeCategoryFilter != null)
            {
                topFilter = new AndFilter(includeCategoryFilter, excludeCategoryFilter);
            }

            TestFilter = topFilter ?? NUnit.Core.TestFilter.Empty;
        }

        public ITestFilter TestFilter { get; private set; }

        private static ITestFilter CreateCategoryFilter(string[] categories)
        {
            if (categories == null || !categories.Any())
            {
                return null;
            }
            return new CategoryFilter(categories);
        }
    }
}