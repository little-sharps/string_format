using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StringFormat
{
    public class TokenStringFormat
    {
        private const string TokenizeRegex = @"\{(?!\{)\w.*?\}";

        private TokenStringFormat(){ }
        private static TokenStringFormat singleton;
        public static TokenStringFormat Singleton
        {
            get { return singleton = singleton ?? new TokenStringFormat(); }
        }

        public string Format(string format, object values)
        {
            return Format(null, format, values);
        }

        public string Format(IFormatProvider provider, string format, object values)
        {
            return Format(provider, format, AnonymousObjectToDictionary(values));
        }

        public string Format(string format, IDictionary<string, object> values)
        {
            return Format(null, format, values);
        }

        public string Format(IFormatProvider provider, string format, IDictionary<string, object> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            IEnumerable<string> tokens;

            var tokenizedString = TokenizeString(format, out tokens);

            return String.Format(provider, tokenizedString, tokens.Select(s => values[s]).ToArray());
        }

        public string TokenizeString(string format)
        {
            IEnumerable<string> junk;

            return TokenizeString(format, out junk);
        }

        public string TokenizeString(string format, out IEnumerable<string> tokens)
        {
            if (format == null)
            {
                throw new ArgumentNullException("format");
            }

            //performance: minimize the number of times the builder will have to "grow", while keeping the initial size reasonable
            var sb = new StringBuilder(format.Length);

            var possibleMatch = Regex.Match(format, TokenizeRegex, RegexOptions.Compiled);

            var tokenList = new List<string>();

            var currentIndex = 0;
            while (possibleMatch.Success)
            {
                if (IsToken(format, possibleMatch))
                {
                    sb.Append(format.Substring(currentIndex, possibleMatch.Index - currentIndex));

                    var fullToken = possibleMatch.ToString();

                    var name = ParseName(fullToken);

                    var index = IndexOfName(tokenList, name);

                    sb.Append(BuildNewToken(fullToken, name, index));

                    currentIndex = possibleMatch.Index + possibleMatch.Length;
                }

                possibleMatch = possibleMatch.NextMatch();
            }

            tokens = tokenList;
            sb.Append(format.Substring(currentIndex));

            return sb.ToString();
        }

        #region Private Methods

        private string ParseName(string fullToken)
        {
            var token = fullToken.Substring(1, fullToken.Length - 2);

            var colonIndex = token.IndexOf(':');

            if (colonIndex >= 0) token = token.Substring(0, colonIndex);

            return token.TrimEnd();
        }

        private int IndexOfName(IList<string> names, string name)
        {
            var index = names.IndexOf(name);

            if (index < 0)
            {
                names.Add(name);
                index = names.IndexOf(name);
            }

            return index;
        }

        private string BuildNewToken(string fullToken, string name, int index)
        {
            fullToken = fullToken.Remove(1, name.Length);

            return fullToken.Insert(1, index.ToString());
        }

        private bool IsToken(string input, Match possibleMatch)
        {
            var consecutiveStartingBraces = 1;
            var currentIndex = possibleMatch.Index;
            while (currentIndex > 0)
            {
                currentIndex--;
                if (input[currentIndex] == '{')
                    consecutiveStartingBraces++;
                else
                    break;
            }

            return consecutiveStartingBraces % 2 == 1;
        }

        private IDictionary<string,object> AnonymousObjectToDictionary(object values)
        {
            var valueDictionary = new Dictionary<string, object>();
            if (values != null)
            {
                foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(values))
                {
                    valueDictionary.Add(propertyDescriptor.Name, propertyDescriptor.GetValue(values));
                }
            }
            return valueDictionary;
        }

        #endregion
    }
}
