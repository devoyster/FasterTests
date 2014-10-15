using FasterTests.Helpers;
using Machine.Specifications;

namespace FasterTests.Tests.Helpers.OptionSpecs
{
    [Subject(typeof(Option<>))]
    public class When_some_option_is_created
    {
        Because of = () =>
            option = Option.Some("string");

        It should_identify_as_some = () => option.IsSome.ShouldBeTrue();

        private static Option<string> option;
    }
}