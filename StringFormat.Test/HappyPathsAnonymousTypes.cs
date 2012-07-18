using System.Globalization;
using NUnit.Framework;
using SharpTestsEx;

namespace StringFormat.Test
{
    [TestFixture]
    public class HappyPathsAnonymousTypes
    {
        private readonly CultureInfo english = CultureInfo.GetCultureInfo("en-US");

        [Test] 
        public void SimpleTest()
        {
            const string test = "The {speed} brown fox jumped over the lazy {animal}.";
            const string expected = "The quick brown fox jumped over the lazy dog.";

            TokenStringFormat.Format(test, new {speed = "quick", animal = "dog"})
                .ShouldEqualWithDiff(expected);
        }

        [Test]
        public void SimpleTestWithFormat()
        {
            const string test = "Your total of {quantity:N0} items comes to a total of {total:C2}";
            const string expected = "Your total of 4,234 items comes to a total of $12,435.89";

            TokenStringFormat.Format(english, test, new {quantity = 4234, total = 12435.89})
                .ShouldEqualWithDiff(expected);
        }

        [Test]
        public void MultilineTest()
        {
            const string test = "This is a {amount}\r\nline test. It would be nice if {library} worked for multiple lines.";
            const string expected = "This is a 2\r\nline test. It would be nice if StringFormat worked for multiple lines.";

            TokenStringFormat.Format(test, new { amount = 2, library = "StringFormat" })
                .ShouldEqualWithDiff(expected);
        }

        [Test]
        public void TrickyBracesTest()
        {
            const string test =
                "Escaping the characters '{{' and '}}' can be tricky. The following will be even more fun: {{!@#$#:39403229010{zero}}} stacks and queues }}}}zz::&*%&$*#{{*{{{one}00{{98}}";
            const string expected =
                "Escaping the characters '{' and '}' can be tricky. The following will be even more fun: {!@#$#:394032290100} stacks and queues }}zz::&*%&$*#{*{100{98}";

            TokenStringFormat.Format(test, new {zero = 0, one = 1, two = 2})
                .ShouldEqualWithDiff(expected);
        }
    }
}