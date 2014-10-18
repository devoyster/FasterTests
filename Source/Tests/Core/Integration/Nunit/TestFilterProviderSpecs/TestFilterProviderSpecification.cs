using FasterTests.Core.Integration.Nunit;
using Machine.Fakes;
using NUnit.Core;

namespace FasterTests.Tests.Core.Integration.Nunit.TestFilterProviderSpecs
{
    public abstract class TestFilterProviderSpecification : WithFakes
    {
        protected static ITest MockTestWithCategories(params string[] categories)
        {
            var test = An<ITest>();
            test.WhenToldTo(t => t.Categories).Return(categories);
            return test;
        }

        protected static TestFilterProvider Subject;
        protected static ITestFilter Result;
    }
}