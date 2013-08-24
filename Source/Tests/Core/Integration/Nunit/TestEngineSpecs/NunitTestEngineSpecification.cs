using System;
using System.Collections.Generic;
using FasterTests.Core.Integration.Nunit;
using FasterTests.Core.Interfaces.Integration;
using FasterTests.Core.Interfaces.Integration.Nunit;
using FasterTests.Core.Interfaces.Models;
using Machine.Fakes;
using System.Linq;
using FasterTests.Helpers;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.TestEngineSpecs
{
    public abstract class NunitTestEngineSpecification : WithSubject<TestEngine>
    {
        Establish context = () =>
        {
            Configure<ITestFrameworkInitializer, TestFrameworkInitializer>();
            WhenToldToSetupForReturn(true);
        };

        protected static TestResult TestResult { get; private set; }

        protected static void RunTest<T>() where T : class
        {
            TestResult = RunTests(typeof(T)).Single();
        }

        protected static IList<TestResult> RunTests(params Type[] testTypes)
        {
            var tests = testTypes.Select(t => new TestDescriptor
                                                  {
                                                      Name = t.FullName,
                                                      AssemblyPath = t.Assembly.Location
                                                  });

            var results = new List<TestResult>();
            Subject.Results.Subscribe(results.Add);

            tests.ForEach(Subject.RunTest);

            return results;
        }

        protected static void WhenToldToSetupForReturn(bool result)
        {
            The<ISetupFixturesContext>()
                .WhenToldTo(c => c.SetupFor(Param.IsAny<TestDescriptor>(), Param.IsAny<IObserver<TestResult>>()))
                .Return(result);
        }
    }
}