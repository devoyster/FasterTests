using System;
using FasterTests.Helpers;
using Machine.Specifications;

namespace FasterTests.Tests.Helpers.OptionSpecs
{
    [Subject(typeof(Option<>))]
    public class When_none_option_value_is_requested
    {
        Establish context = () =>
            option = Option.None<string>();

        Because of = () =>
            exception = Catch.Exception(() => value = option.Value);

        It should_fail = () => exception.ShouldBeOfType<InvalidOperationException>();

        private static Option<string> option;
        private static string value;
        private static Exception exception;
    }
}