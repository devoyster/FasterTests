using System;
using System.Collections.Generic;
using FasterTests.Core.Integration.Nunit;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Core.Interfaces.Models;
using Machine.Fakes;
using Machine.Specifications;
using System.Linq;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.AssemblySetupFixturesContextSpecs
{
    public abstract class AssemblySetupFixturesContextSpecification : WithSubject<AssemblySetupFixturesContext>
    {
        Establish context = () =>
        {
            TestDescriptor = new TestDescriptor();

            cachedFixtures = new Dictionary<Type, ISetupFixture>();

            The<ISetupFixtureFactory>()
                .WhenToldTo(f => f.Create(Param.IsAny<Type>()))
                .Return((Type t) => TheFixtureFor(t));

            The<ISetupFixtureInspector>()
                .WhenToldTo(i => i.LoadAllTypesFrom(Param.IsAny<string>()))
                .Return(() => cachedFixtures.Keys
                                .OrderBy(t => t.Namespace ?? "")
                                .ThenBy(t => t.Name));
        };

        protected static ISetupFixture TheFixtureFor<T>()
        {
            return TheFixtureFor(typeof(T));
        }

        protected static ISetupFixture ConfigureFixtureFor<T>(bool isRequired = false, bool isSetupSucceeds = true)
        {
            var fixture = TheFixtureFor<T>();

            fixture
                .WhenToldTo(f => f.IsRequiredFor(TestDescriptor))
                .Return(isRequired);
            fixture
                .WhenToldTo(f => f.State)
                .Return(isSetupSucceeds ? SetupFixtureState.SetupSucceeded : SetupFixtureState.SetupFailed);

            return fixture;
        }

        private static ISetupFixture TheFixtureFor(Type type)
        {
            ISetupFixture fixture;
            if (!cachedFixtures.TryGetValue(type, out fixture))
            {
                fixture = An<ISetupFixture>();
                cachedFixtures.Add(type, fixture);
            }

            return fixture;
        }

        protected static TestDescriptor TestDescriptor { get; private set; }

        protected static IObserver<TestResult> TheResultsObserver
        {
            get { return The<IObserver<TestResult>>(); }
        }

        private static Dictionary<Type, ISetupFixture> cachedFixtures;
    }
}