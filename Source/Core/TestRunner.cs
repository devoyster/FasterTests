using FasterTests.Core.Implementation.Infrastructure;
using FasterTests.Core.Interfaces;
using FasterTests.Core.Interfaces.Settings;
using SimpleInjector;

namespace FasterTests.Core
{
    public class TestRunner
    {
        public void Run(TestRunSettings settings)
        {
            var container = new Container();
            new DependenciesConfigurator(settings).ConfigureIn(container);

            var entryPoint = container.GetInstance<ITestRunnerEntryPoint>();

            entryPoint.Run();
        }
    }
}