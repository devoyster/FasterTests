using SimpleInjector;

namespace Funt.Core.Infrastructure
{
    public interface IDependenciesConfigurator
    {
        void ConfigureIn(Container container);
    }
}