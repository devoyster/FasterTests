using System;
using FasterTests.Helpers;
using Machine.Specifications;

namespace FasterTests.Tests.Helpers
{
    [Subject(typeof(AppDomainExtensions))]
    public class When_object_is_created_in_another_domain_from_assembly
    {
        Establish context = () =>
            domain = AppDomain.CreateDomain("domain name");

        private Cleanup after = () =>
            AppDomain.Unload(domain);

        Because of = () =>
            domainObject = domain.CreateInstanceFromAndUnwrap<TestCrossDomainObject>();

        It should_call_object_by_reference = () => domainObject.AppDomainName.ShouldEqual("domain name");

        private static AppDomain domain;
        private static TestCrossDomainObject domainObject;
    }
}