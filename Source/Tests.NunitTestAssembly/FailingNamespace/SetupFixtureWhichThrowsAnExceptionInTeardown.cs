using System;
using NUnit.Framework;

namespace FasterTests.Tests.NunitTestAssembly.FailingNamespace
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