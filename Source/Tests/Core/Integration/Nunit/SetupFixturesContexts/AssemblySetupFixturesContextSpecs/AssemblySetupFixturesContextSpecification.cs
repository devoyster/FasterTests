using System;
using System.Collections.Generic;
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
                .WhenToldTo(f => f.CreateAllFrom(Param.IsAny<string>()))
                .Return(() => cachedFixtures.Values
                                .OrderBy(f => f.Type.Namespace ?? "")
                                .ThenBy(f => f.Type.Name));
        };

        protected static ISetupFixture TheFixtureFor<T>()
        {
            return cachedFixtures[typeof(T)];
        }

        protected static ISetupFixture ConfigureFixtureFor<T>(bool isRequired = false,
                                                              bool isSetupSucceeded = false,
                                                              bool isSetupFailed = false)
        {
            var fixture = An<ISetupFixture>();
            cachedFixtures.Add(typeof(T), fixture);

            var state = isSetupSucceeded
                            ? SetupFixtureState.SetupSucceeded
                            : (isSetupFailed ? SetupFixtureState.SetupFailed : SetupFixtureState.NoSetupExecuted);

            fixture
                .WhenToldTo(f => f.Type)
                .Return(typeof(T));
            fixture
                .WhenToldTo(f => f.IsRequiredFor(TestDescriptor))
                .Return(isRequired);
            fixture
                .WhenToldTo(f => f.State)
                .Return(() => state);
            fixture
                .WhenToldTo(f => f.Setup(TheResultsObserver))
                .Callback(() => state = SetupFixtureState.SetupSucceeded);

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