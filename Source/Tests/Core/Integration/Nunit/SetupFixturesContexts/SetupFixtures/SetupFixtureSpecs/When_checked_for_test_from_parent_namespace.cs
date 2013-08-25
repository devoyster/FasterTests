﻿using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures;
using FasterTests.Tests.NunitTestAssembly;
using FasterTests.Tests.NunitTestAssembly.Namespace;
using Machine.Specifications;
using FasterTests.Tests.TestHelpers;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.SetupFixtureSpecs
{
    [Subject(typeof(SetupFixture))]
    public class When_checked_for_test_from_parent_namespace : NunitSetupFixtureSpecification<NamespaceSetupFixture>
    {
        Because of = () =>
            isRequired = Subject.IsRequiredFor(typeof(PassingTest).GetTestDescriptor());

        It should_not_be_required = () => isRequired.ShouldBeFalse();

        private static bool isRequired;
    }
}