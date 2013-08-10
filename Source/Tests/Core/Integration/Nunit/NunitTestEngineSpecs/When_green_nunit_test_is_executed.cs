﻿using FasterTests.Core.Implementation.Integration.Nunit;
using FasterTests.Tests.NunitTestAssembly;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.NunitTestEngineSpecs
{
    [Subject(typeof(NunitTestEngine))]
    public class When_green_nunit_test_is_executed : NunitTestEngineSpecification
    {
        Because of = () =>
            RunTest<PassingTest>();

        It should_pass = () => TestResult.IsSuccess.ShouldBeTrue();
    }
}