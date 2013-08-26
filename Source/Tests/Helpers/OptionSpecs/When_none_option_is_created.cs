using FasterTests.Helpers;
using Machine.Specifications;

namespace FasterTests.Tests.Helpers.OptionSpecs
{
    [Subject(typeof(Option<>))]
    public class When_none_option_is_created
    {
        Because of = () =>
            option = Option.None<string>();

        It should_identify_as_none = () => option.IsSome.ShouldBeFalse();

        private static Option<string> option;
    }
}