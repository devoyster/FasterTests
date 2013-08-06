using SimpleInjector;

namespace FasterTests.Core.Infrastructure
{
    public interface IDependenciesConfigurator
    {
        void ConfigureIn(Container container);
    }
}