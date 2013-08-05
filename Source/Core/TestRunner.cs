using Funt.Core.Infrastructure.Implementation;
using Funt.Core.Models;
using SimpleInjector;

namespace Funt.Core
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