using System;
using System.Threading;
using FasterTests.Core.Interfaces.Models;

namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures
{
    public class SetupFixture : ISetupFixture
    {
        private readonly Type _type;
        private readonly Lazy<ISetupFixtureAdapter> _lazyAdapter;

        public SetupFixture(Type type, ISetupFixtureAdapterFactory adapterFactory)
        {
            _type = type;
            _lazyAdapter = new Lazy<ISetupFixtureAdapter>(
                () => adapterFactory.Create(type),
                LazyThreadSafetyMode.None);
        }

        public Type Type
        {
            get { return _type; }
        }

        public SetupFixtureState State { get; private set; }

        public bool IsRequiredFor(TestDescriptor test)
        {
            return IsRequiredFor(test.Name);
        }

        public bool IsRequiredFor(ISetupFixture otherFixture)
        {
            return IsRequiredFor(otherFixture.Type.FullName);
        }

        private bool IsRequiredFor(string typeFullName)
        {
            return string.IsNullOrEmpty(_type.Namespace) || typeFullName.StartsWith(_type.Namespace + Type.Delimiter);
        }

        public void SetParentFailed(IObserver<TestResult> resultsObserver)
        {
            if (State != SetupFixtureState.NoSetupExecuted)
            {
                throw new InvalidOperationException("Fixture was already set up");
            }

            State = SetupFixtureState.SetupFailed;
        }

        public void Setup(IObserver<TestResult> resultsObserver)
        {
            if (State != SetupFixtureState.NoSetupExecuted)
            {
                throw new InvalidOperationException("Fixture was already set up");
            }

            var isSucccess = _lazyAdapter.Value.Setup();

            State = isSucccess ? SetupFixtureState.SetupSucceeded : SetupFixtureState.SetupFailed;
        }

        public void Teardown(IObserver<TestResult> resultsObserver)
        {
            switch (State)
            {
                case SetupFixtureState.NoSetupExecuted:
                    throw new InvalidOperationException("Fixture was not set up");

                case SetupFixtureState.SetupSucceeded:
                    _lazyAdapter.Value.Teardown();
                    break;
            }

            State = SetupFixtureState.NoSetupExecuted;
        }
    }
}