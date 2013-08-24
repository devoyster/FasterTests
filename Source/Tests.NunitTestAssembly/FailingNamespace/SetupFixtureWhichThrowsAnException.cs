using System;
using NUnit.Framework;

namespace FasterTests.Tests.NunitTestAssembly.FailingNamespace
{
    [SetUpFixture]
    public class SetupFixtureWhichThrowsAnException
    {
        public static bool TeardownWasInvoked { get; set; }

        [SetUp]
        public void Setup()
        {
            throw new Exception();
        }

        [TearDown]
        public void Teardown()
        {
            TeardownWasInvoked = true;
        }
    }
}