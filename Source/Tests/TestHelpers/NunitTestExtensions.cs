using System;
using FasterTests.Core.Interfaces.Models;

namespace FasterTests.Tests.TestHelpers
{
    public static class NunitTestExtensions
    {
        public static TestDescriptor GetTestDescriptor(this Type testType)
        {
            return new TestDescriptor
                       {
                           Name = testType.FullName,
                           AssemblyPath = testType.Assembly.Location
                       };
        }
    }
}