﻿using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures;
using FasterTests.Tests.NunitTestAssembly;
using FasterTests.Tests.NunitTestAssembly.Namespace;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.SetupFixtureSpecs
{
    [Subject(typeof(SetupFixture))]
    public class When_checked_for_fixture_from_parent_namespace : SetupFixtureSpecification<NamespaceSetupFixture>
    {
        Because of = () =>
            isRequired = Subject.IsRequiredFor(CreateFixtureFor<RootSetupFixture>());

        It should_not_be_required = () => isRequired.ShouldBeFalse();

        private static bool isRequired;
    }
}