using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StringFormat.Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            
            //var test = "a{{{{12axaq }a{ppazw}}}";
            var test = "John was born on {dob:D}; he weighed {weight} lbs. and was {length} inches long! The doctors confirmed that he weighed {weight} lbs.";
            //var test = "abcdefghijklmnop";

            Console.WriteLine(
                StringFormat.TokenStringFormat.Singleton.Format(test,
                    new {dob = DateTime.Now, weight = 11, length = 21}));


            Console.WriteLine(TokenStringFormat.Singleton.TokenizeString(test));
        }
    }
}
