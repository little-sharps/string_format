using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NUnit.Framework;
using SharpTestsEx;

namespace StringFormat.Test
{
    [TestFixture]
    public class ExpectedExceptions
    {
        [Test]
        public void KeyNotFoundException()
         {
             Executing.This(() => TokenStringFormat.Format("My name is {name}", new {foo = "Jack"}))
                 .Should().Throw<KeyNotFoundException>();

         }

        [Test]
        public void FormatException()
        {
            Executing.This(
                () => TokenStringFormat.Format("I should have escaped this curly brace: {", new {Foo = "bar"}))
                .Should().Throw<FormatException>();

            Executing.This(
                () => TokenStringFormat.Format("A token cannot span {\r\nname} multiple lines.", new { Foo = "bar" }))
                .Should().Throw<FormatException>();
        }

        [Test]
        public void ArgumentNullException()
        {
            Executing.This(() => TokenStringFormat.Format(null, new {Foo = "bar"}))
                .Should("Format parameter").Throw<ArgumentNullException>();

            object test = null;
            Executing.This(() => TokenStringFormat.Format("abc", test))
                .Should("Object parameter").NotThrow();

            IDictionary<string, object> dictionary = null;
            Executing.This(() => TokenStringFormat.Format("abc", dictionary))
                .Should("Dictionary parameter").Throw<ArgumentNullException>();
        }
    }
}