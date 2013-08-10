using FasterTests.Core;
using FasterTests.Tests.Core.ServiceContainerSpecs.TestServices;
using Machine.Specifications;

namespace FasterTests.Tests.Core.ServiceContainerSpecs
{
    [Subject(typeof(ServiceContainer))]
    public class When_named_services_are_registered
    {
        Establish context = () =>
        {
            container = new ServiceContainer();

            container.Register("string #1", "first");
            container.Register("string #2", "second");

            container.Register<ServiceWithNamedParameters>();
        };

        Because of = () =>
            service = container.GetInstance<ServiceWithNamedParameters>();

        It should_inject_first_dependency_by_name = () => service.First.ShouldEqual("string #1");

        It should_inject_second_dependency_by_name = () => service.Second.ShouldEqual("string #2");

        private static ServiceContainer container;
        private static ServiceWithNamedParameters service;
    }
}