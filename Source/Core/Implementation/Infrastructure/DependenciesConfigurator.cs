using System;
using FasterTests.Core.Implementation.Integration.Nunit;
using FasterTests.Core.Implementation.Workers;
using FasterTests.Core.Interfaces;
using FasterTests.Core.Interfaces.Infrastructure;
using FasterTests.Core.Interfaces.Integration;
using FasterTests.Core.Interfaces.Models;
using FasterTests.Core.Interfaces.Workers;
using SimpleInjector;

namespace FasterTests.Core.Implementation.Infrastructure
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