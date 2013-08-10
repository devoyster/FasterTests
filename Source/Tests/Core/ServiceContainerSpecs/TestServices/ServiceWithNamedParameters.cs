namespace FasterTests.Tests.Core.ServiceContainerSpecs.TestServices
{
    public class ServiceWithNamedParameters
    {
        private readonly string _first;
        private readonly string _second;

        public ServiceWithNamedParameters(string first, string second)
        {
            _first = first;
            _second = second;
        }

        public string First
        {
            get { return _first; }
        }

        public string Second
        {
            get { return _second; }
        }
    }
}