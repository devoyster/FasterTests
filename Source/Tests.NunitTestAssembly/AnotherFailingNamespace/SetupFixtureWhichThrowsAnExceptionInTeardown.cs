using System;
using NUnit.Framework;

namespace FasterTests.Tests.NunitTestAssembly.AnotherFailingNamespace
{
    [SetUpFixture]
    public class SetupFixtureWhichThrowsAnExceptionInTeardown
    {
        [TearDown]
        public void Teardown()
        {
            throw new Exception();
        }
    }
}