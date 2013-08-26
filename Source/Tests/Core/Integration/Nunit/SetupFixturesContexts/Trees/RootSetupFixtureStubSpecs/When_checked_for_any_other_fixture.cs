using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.Trees;
using Machine.Fakes;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.Trees.RootSetupFixtureStubSpecs
{
    [Subject(typeof(RootSetupFixtureStub))]
    public class When_checked_for_any_other_fixture : WithSubject<RootSetupFixtureStub>
    {
        Because of = () =>
            isRequired = Subject.IsRequiredFor((ISetupFixture)null);

        It should_be_required = () => isRequired.ShouldBeTrue();

        private static bool isRequired;
    }
}