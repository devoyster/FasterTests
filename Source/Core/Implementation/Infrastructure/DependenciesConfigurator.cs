using System;
using FasterTests.Core.Implementation.Integration.Nunit;
using FasterTests.Core.Implementation.Workers;
using FasterTests.Core.Interfaces;
using FasterTests.Core.Interfaces.Integration;
using FasterTests.Core.Interfaces.Settings;
using FasterTests.Core.Interfaces.Workers;
using SimpleInjector;

namespace FasterTests.Core.Implementation.Infrastructure
{
    public class DependenciesConfigurator
    {
        private readonly TestRunSettings _settings;

        public DependenciesConfigurator(TestRunSettings settings)
        {
            _settings = settings;
        }

        public Container ConfigureContainer()
        {
            var container = new Container();

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

            return container;
        }
    }
}