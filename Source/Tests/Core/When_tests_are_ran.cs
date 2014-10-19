using System;
using System.IO;
using FasterTests.Core;
using FasterTests.Core.Interfaces;
using FasterTests.Tests.NunitTestAssembly.Properties;
using Machine.Specifications;

namespace FasterTests.Tests.Core
{
    [Subject(typeof(TestRunner))]
    public class When_tests_are_ran
    {
        Establish context = () =>
        {
            subject = new TestRunner();

            output = new StringWriter();
            settings = new TestRunSettings
                           {
                               AssemblyPath = typeof(NunitTestAssemblyMarker).Assembly.Location,
                               DegreeOfParallelism = Environment.ProcessorCount,
                               Output = output
                           };
        };

        Because of = () =>
            exception = Catch.Exception(() => subject.Run(settings));

        It should_not_throw_exceptions = () => exception.ShouldBeNull();

        It should_run_some_tests = () => output.ToString().ShouldMatch("Tests run: [1-9],");

        private static TestRunner subject;
        private static StringWriter output;
        private static TestRunSettings settings;
        private static Exception exception;
    }
}