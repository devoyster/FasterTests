using FasterTests.Core.Infrastructure;
using FasterTests.Core.Interfaces;

namespace FasterTests.Core
{
    public class TestRunner : ITestRunner
    {
        public void Run(TestRunSettings settings)
        {
            var container = new DependenciesConfigurator(settings).ConfigureContainer();

            var bootstrapper = container.GetInstance<ITestRunnerBootstrapper>();
            bootstrapper.Run();
        }
    }
}