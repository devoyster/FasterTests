using System;
using Funt.Core.Implementation;
using Funt.Core.Integration;
using Funt.Core.Integration.Implementation.Nunit;
using Funt.Core.Models;
using Funt.Core.Workers;
using Funt.Core.Workers.Implementation;
using SimpleInjector;

namespace Funt.Core.Infrastructure.Implementation
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