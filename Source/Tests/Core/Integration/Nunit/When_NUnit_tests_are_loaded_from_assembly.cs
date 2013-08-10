﻿using System.Collections.Generic;
using FasterTests.Core.Implementation.Integration.Nunit;
using FasterTests.Tests.NunitInspectorTestAssembly;
using FasterTests.Tests.NunitInspectorTestAssembly.Namespace;
using FasterTests.Tests.NunitInspectorTestAssembly.Properties;
using Machine.Specifications;
using System.Linq;

namespace FasterTests.Tests.Core.Integration.Nunit
{
    [Subject(typeof(NunitTestInspector))]
    public class When_nunit_tests_are_loaded_from_assembly
    {
        Establish context = () =>
        {
            subject = new NunitTestInspector();
            nunitTestAssemblyPath = typeof(Marker).Assembly.Location;
        };

        Because of = () =>
            testNames = subject.LoadAllTestsFrom(nunitTestAssemblyPath).Select(d => d.Name).ToList();

        It should_load_all_tests_only = () => testNames.ShouldContainOnly(typeof(TestsWithTestFixture).FullName,
                                                                          typeof(TestsWithoutTestFixture).FullName,
                                                                          typeof(TestsInNamespace).FullName);

        It should_not_load_setup_fixtures = () => testNames.ShouldNotContain(typeof(RootSetupFixture).FullName,
                                                                             typeof(NamespaceSetupFixture).FullName);

        private static NunitTestInspector subject;
        private static string nunitTestAssemblyPath;
        private static List<string> testNames;
    }
}