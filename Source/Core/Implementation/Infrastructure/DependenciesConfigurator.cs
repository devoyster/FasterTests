using System;
using FasterTests.Core.Interfaces.Settings;

namespace FasterTests.Core.Implementation.Infrastructure
{
    internal class DependenciesConfigurator
    {
        private readonly TestRunSettings _settings;

        public DependenciesConfigurator(TestRunSettings settings)
        {
            _settings = settings;
        }

        public IServiceContainer ConfigureContainer()
        {
            var container = new ServiceContainer();

            container.Register(_settings.AssemblyPath, "testAssemblyPath");
            container.Register(_settings.NoParallelGroups, "noParallelGroups");
            container.Register(_settings.ConfigStringsToPatch, "configStringsToPatch");

            container.Register(Console.Out, "output");

            container.RegisterAssembly(GetType().Assembly);

            return container;
        }
    }
}