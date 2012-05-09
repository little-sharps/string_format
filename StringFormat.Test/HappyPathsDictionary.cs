using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;

namespace StringFormat.Test
{
    [TestFixture]
    public class HappyPathsDictionary
    {
        private CultureInfo english = CultureInfo.GetCultureInfo("en-US");

        [Test]
        public void SimpleTest()
        {
            const string test = "The {speed} brown fox jumped over the lazy {animal}.";
            const string expected = "The quick brown fox jumped over the lazy dog.";

            var values = new Dictionary<string, object>
                             {
                                 {"speed", "quick"},
                                 {"animal", "dog"},
                             };

            TokenStringFormat.Format(test, values)
                .ShouldEqualWithDiff(expected);
        }

        [Test]
        public void SimpleTestWithFormat()
        {
            const string test = "{name}, your total of {quantity:N0} items comes to a total of {total:C2}";
            const string expected = "John, your total of 4,234 items comes to a total of $12,435.89";

            var values = new Dictionary<string, object>
                             {
                                 {"quantity", 4234},
                                 {"total", 12435.89},
                                 {"name", "John"},
                             };

            TokenStringFormat.Format(english, test, values)
                .ShouldEqualWithDiff(expected);
        }

        [Test]
        public void TrickyBracesTest()
        {
            const string test =
                "Escaping the characters '{{' and '}}' can be tricky. The following will be even more fun: {{!@#$#:39403229010{one}}} stacks and queues }}}}zz::&*%&$*#{{*{{{zero}00{{98}}";
            const string expected =
                "Escaping the characters '{' and '}' can be tricky. The following will be even more fun: {!@#$#:394032290101} stacks and queues }}zz::&*%&$*#{*{000{98}";

            var values = new Dictionary<string, object>
                             {
                                 {"zero", 0},
                                 {"one", 1},
                                 {"two", 2}
                             };

            TokenStringFormat.Format(test, values)
                .ShouldEqualWithDiff(expected);
        }     
    }
}