using SimpleInjector;

namespace FasterTests.Core.Interfaces.Infrastructure
{
    public interface IDependenciesConfigurator
    {
        void ConfigureIn(Container container);
    }
}