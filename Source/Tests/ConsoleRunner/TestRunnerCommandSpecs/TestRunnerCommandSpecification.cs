using System.IO;
using FasterTests.ConsoleRunner;
using Machine.Fakes;
using Machine.Specifications;

namespace FasterTests.Tests.ConsoleRunner.TestRunnerCommandSpecs
{
    public abstract class TestRunnerCommandSpecification : WithSubject<TestRunnerCommand>
    {
        Establish context = () =>
            Output = new StringWriter();

        protected static StringWriter Output { get; private set; }
    }
}