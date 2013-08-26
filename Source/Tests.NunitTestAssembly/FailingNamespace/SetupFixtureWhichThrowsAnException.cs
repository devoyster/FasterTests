using System;
using NUnit.Framework;

namespace FasterTests.Tests.NunitTestAssembly.FailingNamespace
{
    [SetUpFixture]
    public class SetupFixtureWhichThrowsAnException
    {
        [SetUp]
        public void Setup()
        {
            throw new Exception();
        }
    }
}