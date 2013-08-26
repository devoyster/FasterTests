using System;
using FasterTests.Core.Integration.Nunit.SetupFixturesContexts;
using FasterTests.Core.Interfaces.Models;
using FasterTests.Helpers.Trees;
using Machine.Fakes;
using Machine.Specifications;

namespace FasterTests.Tests.Core.Integration.Nunit.SetupFixturesContexts.AssemblySetupFixturesContextSpecs
{
    public abstract class AssemblySetupFixturesContextSpecification : WithSubject<AssemblySetupFixturesContext>
    {
        Establish context = () =>
            TestDescriptor = new TestDescriptor();

        protected static ISetupFixture CreateFixture(bool isRequired = false,
                                                     bool isSetupSucceeded = false,
                                                     bool isSetupFailed = false)
        {
            var fixture = An<ISetupFixture>();

            var state = isSetupSucceeded
                            ? SetupFixtureState.SetupSucceeded
                            : (isSetupFailed ? SetupFixtureState.SetupFailed : SetupFixtureState.NoSetupExecuted);
            SetFixtureState(fixture, state);

            fixture
                .WhenToldTo(f => f.IsRequiredFor(TestDescriptor))
                .Return(isRequired);
            fixture
                .WhenToldTo(f => f.Setup(TheResultsObserver))
                .Callback(() => SetFixtureState(fixture, SetupFixtureState.SetupSucceeded));

            return fixture;
        }

        protected static void SetFixtureState(ISetupFixture fixture, SetupFixtureState state)
        {
            fixture
                .WhenToldTo(f => f.State)
                .Return(() => state);
        }

        protected static void ConfigureTreeBuilder(ITree<ISetupFixture> tree)
        {
            The<ISetupFixtureTreeBuilder>()
                .WhenToldTo(f => f.BuildFrom(Param.IsAny<string>()))
                .Return(tree);
        }

        protected static TestDescriptor TestDescriptor { get; private set; }

        protected static IObserver<TestResult> TheResultsObserver
        {
            get { return The<IObserver<TestResult>>(); }
        }

        protected static ISetupFixture RootFixture
        {
            get { return CreateFixture(isRequired: true, isSetupSucceeded: true); }
        }
    }
}