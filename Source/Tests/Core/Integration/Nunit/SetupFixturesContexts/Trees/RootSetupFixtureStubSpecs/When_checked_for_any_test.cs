using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.Trees;
using FasterTests.Core.Interfaces.Models;
using Machine.Fakes;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.Trees.RootSetupFixtureStubSpecs
{
    [Subject(typeof(RootSetupFixtureStub))]
    public class When_checked_for_any_test : WithSubject<RootSetupFixtureStub>
    {
        Because of = () =>
            isRequired = Subject.IsRequiredFor((TestDescriptor)null);

        It should_be_required = () => isRequired.ShouldBeTrue();

        private static bool isRequired;
    }
}