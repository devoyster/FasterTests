using FasterTests.Core.Implementation.Infrastructure;
using FasterTests.Core.Interfaces;
using FasterTests.Core.Interfaces.Settings;

namespace FasterTests.Core
{
    public class TestRunner
    {
        public void Run(TestRunSettings settings)
        {
            var container = new DependenciesConfigurator(settings).ConfigureContainer();

            var entryPoint = container.GetInstance<ITestRunnerBootstrapper>();

            entryPoint.Run();
        }
    }
}