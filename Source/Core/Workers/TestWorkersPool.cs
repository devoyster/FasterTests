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
        private readonly string[] _configStringsToPatch;
        private int _workerIndex;

        public TestWorkersPool(string[] configStringsToPatch)
        {
            _configStringsToPatch = configStringsToPatch;
        }

        public IObservable<TestResult> RunTestsAsync(IEnumerable<TestDescriptor> tests)
        {
            var assemblyPath = tests.First().AssemblyPath;
            var domainSettings = new AppDomainSetup
                                     {
                                         ApplicationBase = Path.GetDirectoryName(assemblyPath),
                                         ConfigurationFile = PatchConfigFile(Path.GetFileName(assemblyPath) + ".config")
                                     };

            var domain = AppDomain.CreateDomain("Funt.Worker", null, domainSettings);

            var worker = domain.CreateInstanceAndUnwrap<AppDomainWorker>();
            worker.InstallAssemblyResolve();

            return worker.RunTestsAsync(tests);
        }

        private string PatchConfigFile(string originalConfigFilePath)
        {
            _workerIndex++;
            if (_configStringsToPatch == null || _workerIndex == 1)
            {
                return originalConfigFilePath;
            }

            var configText = File.ReadAllText(originalConfigFilePath);
            configText = _configStringsToPatch.Aggregate(configText,
                                                         (config, toPatch) => config.Replace(toPatch, toPatch + "_" + _workerIndex));

            var newConfigFilePath = Path.GetTempFileName();
            File.WriteAllText(newConfigFilePath, configText);

            return newConfigFilePath;
        }
    }
}