﻿using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Tests.NunitTestAssembly.AnotherNamespace;
using FasterTests.Tests.NunitTestAssembly.Namespace;
using Machine.Specifications;
using Machine.Fakes;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.AssemblySetupFixturesContextSpecs
{
    [Subject(typeof(AssemblySetupFixturesContext))]
    public class When_setup_is_performed : AssemblySetupFixturesContextSpecification
    {
        Establish context = () =>
        {
            ConfigureFixtureFor<NamespaceSetupFixture>(isRequired: true);
            ConfigureFixtureFor<AnotherNamespaceSetupFixture>();
        };

        Because of = () =>
            Subject.SetupFor(TestDescriptor, TheResultsObserver);

        It should_setup_required_fixture = () => TheFixtureFor<NamespaceSetupFixture>().WasToldTo(f => f.Setup(TheResultsObserver)).OnlyOnce();

        It should_skip_not_required_fixture = () => TheFixtureFor<AnotherNamespaceSetupFixture>().WasNotToldTo(f => f.Setup(TheResultsObserver));
    }
}