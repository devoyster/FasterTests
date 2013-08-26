using System.Collections.Generic;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.Trees;
using FasterTests.Helpers.Trees;
using Machine.Fakes;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.Trees.SetupFixtureTreeBuilderSpecs
{
    public abstract class SetupFixtureTreeBuilderSpecification : WithSubject<SetupFixtureTreeBuilder>
    {
        Establish context = () =>
        {
            AssemblyPath = "path";

            fixtures = new List<ISetupFixture>();

            The<ISetupFixtureFactory>()
                .WhenToldTo(f => f.CreateAllFrom(AssemblyPath))
                .Return(fixtures);
        };

        protected static ISetupFixture AddFixtureFor<T>()
        {
            var fixture = An<ISetupFixture>();
            fixtures.Add(fixture);

            fixture
                .WhenToldTo(f => f.Type)
                .Return(typeof(T));
            fixture
                .WhenToldTo(f => f.IsRequiredFor(Param.IsAny<ISetupFixture>()))
                .Return((ISetupFixture otherFixture) => otherFixture.Type.FullName.StartsWith(fixture.Type.Namespace ?? string.Empty));

            return fixture;
        }

        protected static string AssemblyPath { get; private set; }
        protected static ITree<ISetupFixture> Tree { get; set; }

        private static List<ISetupFixture> fixtures;
    }
}