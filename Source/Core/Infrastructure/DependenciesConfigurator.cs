using System;
using FasterTests.Core.Interfaces.Settings;
using LightInject;

namespace FasterTests.Core.Infrastructure
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

            _settings.Output = _settings.Output ?? Console.Out;
            container.RegisterInstance(_settings);

            container.RegisterAssembly(GetType().Assembly);

            return container;
        }
    }
}