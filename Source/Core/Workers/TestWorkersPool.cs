using System;
using System.Collections.Generic;
using Funt.Core.Models;
using Funt.Helpers;

namespace Funt.Core.Workers
{
    public class TestWorkersPool : MarshalByRefObject
    {
        public IObservable<TestResult> RunTestsAsync(IEnumerable<TestDescriptor> tests)
        {
            var domain = AppDomain.CreateDomain("Funt.Worker");
            var worker = domain.CreateInstanceAndUnwrap<AppDomainWorker>();

            return worker.RunTestsAsync(tests);
        }
    }
}