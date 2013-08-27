using System;
using System.Threading;
using FasterTests.Core.Interfaces.Models;

namespace FasterTests.Core.Integration.Nunit.SetupFixturesContexts.SetupFixtures
{
    public class SetupFixture : ISetupFixture
    {
        private readonly Type _type;
        private ISetupFixtureState _state;

        public SetupFixture(Type type, ISetupFixtureAdapterFactory adapterFactory)
        {
            _type = type;

            var lazyAdapter = new Lazy<ISetupFixtureAdapter>(
                                () => adapterFactory.Create(type),
                                LazyThreadSafetyMode.None);
            _state = new NotExecutedSetupFixtureState(lazyAdapter);
        }

        public Type Type
        {
            get { return _type; }
        }

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

        public bool IsExecuted
        {
            get { return _state.IsExecuted; }
        }

        public bool IsSucceeded
        {
            get { return _state.IsSucceeded; }
        }

        public bool IsFailed
        {
            get { return _state.IsFailed; }
        }

        public void SetParentFailed(IObserver<TestResult> resultsObserver)
        {
            _state.SetParentFailed(resultsObserver);
            UpdateState();
        }

        public void Setup(IObserver<TestResult> resultsObserver)
        {
            _state.Setup(resultsObserver);
            UpdateState();
        }

        public void Teardown(IObserver<TestResult> resultsObserver)
        {
            _state.Teardown(resultsObserver);
            UpdateState();
        }

        private void UpdateState()
        {
            _state = _state.NextState();
        }
    }
}