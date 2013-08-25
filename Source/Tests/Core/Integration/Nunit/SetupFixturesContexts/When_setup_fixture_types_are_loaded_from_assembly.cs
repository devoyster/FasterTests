using System;
using System.Collections.Generic;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Tests.NunitInspectorTestAssembly;
using FasterTests.Tests.NunitInspectorTestAssembly.Namespace;
using FasterTests.Tests.NunitInspectorTestAssembly.Properties;
using Machine.Specifications;
using System.Linq;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts
{
    [Subject(typeof(SetupFixtureTypeInspector))]
    public class When_setup_fixture_types_are_loaded_from_assembly
    {
        Establish context = () =>
        {
            subject = new SetupFixtureTypeInspector();
            nunitTestAssemblyPath = typeof(NunitInspectorTestAssemblyMarker).Assembly.Location;
        };

        Because of = () =>
            types = subject.LoadAllFrom(nunitTestAssemblyPath).ToList();

        It should_load_all_setup_fixture_types_only = () => types.ShouldContainOnly(typeof(RootSetupFixture),
                                                                                    typeof(NamespaceSetupFixture));

        It should_not_load_test_types = () => types.ShouldNotContain(typeof(TestsWithTestFixture),
                                                                     typeof(TestsWithoutTestFixture),
                                                                     typeof(TestsInNamespace));

        private static SetupFixtureTypeInspector subject;
        private static string nunitTestAssemblyPath;
        private static List<Type> types;
    }
}