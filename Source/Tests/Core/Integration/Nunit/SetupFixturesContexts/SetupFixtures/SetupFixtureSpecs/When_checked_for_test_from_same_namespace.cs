﻿using FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Specifications;
using FasterTests.Tests.TestHelpers;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures.SetupFixtureSpecs
{
    [Subject(typeof(SetupFixture))]
    public class When_checked_for_test_from_same_namespace : SetupFixtureSpecification<RootSetupFixture>
    {
        Because of = () =>
            isRequired = Subject.IsRequiredFor(typeof(PassingTest).GetTestDescriptor());

        It should_be_required = () => isRequired.ShouldBeTrue();

        private static bool isRequired;
    }
}