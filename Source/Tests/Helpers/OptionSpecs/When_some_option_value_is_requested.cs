using FasterTests.Helpers;
using Machine.Specifications;

namespace FasterTests.Tests.Helpers.OptionSpecs
{
    [Subject(typeof(Option<>))]
    public class When_some_option_value_is_requested
    {
        Establish context = () =>
            option = Option.Some("string");

        Because of = () =>
            value = option.Value;

        It should_return_value_supplied_on_creation = () => option.Value.ShouldEqual("string");

        private static Option<string> option;
        private static string value;
    }
}