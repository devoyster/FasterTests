using System;
using FasterTests.Core.Implementation;
using FasterTests.Core.Integration;
using FasterTests.Core.Integration.Implementation.Nunit;
using FasterTests.Core.Models;
using FasterTests.Core.Workers;
using FasterTests.Core.Workers.Implementation;
using SimpleInjector;

namespace FasterTests.Core.Infrastructure.Implementation
{
    public class DependenciesConfigurator : IDependenciesConfigurator
    {
        private readonly TestRunSettings _settings;

        public DependenciesConfigurator(TestRunSettings settings)
        {
            _settings = settings;
        }

        public void ConfigureIn(Container container)
        {
            container.Register<ITestRunnerEntryPoint>(() => new TestRunnerEntryPoint(_settings.AssemblyPath,
                                                                                     container.GetInstance<ITestInspector>(),
                                                                                     container.GetInstance<ITestDispatcher>(),
                                                                                     container.GetInstance<ITestResultsConsumer>()));

            container.Register<ITestDispatcher>(() => new TestDispatcher(container.GetInstance<ITestWorkersPool>(),
                                                                         _settings.NoParallelGroups));

            container.Register<ITestResultsConsumer>(() => new TestResultsConsoleWriter(Console.Out));

            container.Register<ITestWorkersPool>(() => new TestWorkersPool(_settings.ConfigStringsToPatch));

            container.Register<ITestInspector, NunitTestInspector>();
            container.Register<ITestEngine, NunitTestEngine>();

            container.Verify();
        }
    }
}