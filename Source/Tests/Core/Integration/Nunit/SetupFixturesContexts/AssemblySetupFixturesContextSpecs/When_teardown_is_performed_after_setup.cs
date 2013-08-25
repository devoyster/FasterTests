﻿using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Tests.NunitTestAssembly.AnotherNamespace;
using FasterTests.Tests.NunitTestAssembly.Namespace;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.AssemblySetupFixturesContextSpecs
{
    [Subject(typeof(AssemblySetupFixturesContext))]
    public class When_teardown_is_performed_after_setup : AssemblySetupFixturesContextSpecification
    {
        Establish context = () =>
        {
            ConfigureFixtureFor<NamespaceSetupFixture>(isRequired: true);
            ConfigureFixtureFor<AnotherNamespaceSetupFixture>();

            Subject.SetupFor(TestDescriptor, TheResultsObserver);
        };

        Because of = () =>
            Subject.TeardownAll(TheResultsObserver);

        It should_teardown_required_fixture = () => TheFixtureFor<NamespaceSetupFixture>().WasToldTo(f => f.Teardown(TheResultsObserver)).OnlyOnce();

        It should_skip_not_required_fixture = () => TheFixtureFor<AnotherNamespaceSetupFixture>().WasNotToldTo(f => f.Teardown(TheResultsObserver));
    }
}