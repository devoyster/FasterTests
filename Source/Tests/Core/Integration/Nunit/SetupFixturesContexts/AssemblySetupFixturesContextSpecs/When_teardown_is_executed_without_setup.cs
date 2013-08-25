using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.AssemblySetupFixturesContextSpecs
{
    [Subject(typeof(AssemblySetupFixturesContext))]
    public class When_teardown_is_executed_without_setup : AssemblySetupFixturesContextSpecification
    {
        Because of = () =>
            Subject.TeardownAll(TheResultsObserver);

        It should_succeed = () => {};
    }
}