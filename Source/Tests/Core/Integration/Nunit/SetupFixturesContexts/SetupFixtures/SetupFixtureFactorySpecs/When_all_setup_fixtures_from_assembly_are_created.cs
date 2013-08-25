using System.Collections.Generic;
using FasterTests.Core.Integration.Nunit;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures;
using FasterTests.Tests.NunitTestAssembly;
using FasterTests.Tests.NunitTestAssembly.Namespace;
using Machine.Fakes;
using Machine.Specifications;
using System.Linq;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.SetupFixtureFactorySpecs
{
    [Subject(typeof(SetupFixtureFactory))]
    public class When_all_setup_fixtures_from_assembly_are_created : WithSubject<SetupFixtureFactory>
    {
        Establish context = () =>
        {
            assemblyPath = "test";

            The<ISetupFixtureTypeInspector>()
                .WhenToldTo(i => i.LoadAllFrom(assemblyPath))
                .Return(new[] { typeof(RootSetupFixture), typeof(NamespaceSetupFixture) });
        };

        Because of = () =>
            setupFixtures = Subject.CreateAllFrom(assemblyPath);

        It should_create_instances_of_setup_fixture = () => setupFixtures.ShouldEachConformTo(f => f is SetupFixture);

        It should_initialize_first_fixture_with_supplied_type = () => setupFixtures.First().Type.ShouldEqual(typeof(RootSetupFixture));

        It should_initialize_second_fixture_with_supplied_type = () => setupFixtures.ElementAt(1).Type.ShouldEqual(typeof(NamespaceSetupFixture));

        private static string assemblyPath;
        private static IEnumerable<ISetupFixture> setupFixtures;
    }
}