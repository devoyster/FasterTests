using System;
using NUnit.Framework;

namespace FasterTests.Tests.NunitTestAssembly
{
    public class TestWhichThrowsAnException
    {
        [Test]
        public void Method()
        {
            throw new Exception();
        }
    }
}