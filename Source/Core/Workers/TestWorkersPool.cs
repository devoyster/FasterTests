using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Funt.Core.Models;
using Funt.Helpers;

namespace Funt.Core.Workers
{
    public class TestWorkersPool : MarshalByRefObject
    {
        public IObservable<TestResult> RunTestsAsync(IEnumerable<TestDescriptor> tests)
        {
            var assemblyPath = tests.First().AssemblyPath;
            var domainSettings = new AppDomainSetup
                                     {
                                         ApplicationBase = Path.GetDirectoryName(assemblyPath),
                                         ConfigurationFile = Path.GetFileName(assemblyPath) + ".config"
                                     };
            var domain = AppDomain.CreateDomain("Funt.Worker", null, domainSettings);

            var worker = domain.CreateInstanceAndUnwrap<AppDomainWorker>();
            worker.InstallAssemblyResolve();

            return worker.RunTestsAsync(tests);
        }
    }
}