using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CardGame_Game.Helpers
{
    public static class StringHelper
    {
        #region Public_Methods

        public static IEnumerable<string> EverythingBetween(this string source, string start, string end)
        {
            string pattern = string.Format(
                "{0}({1}){2}",
                Regex.Escape(start),
                ".+?",
                 Regex.Escape(end));

            return GetRegexMatches(source, pattern);
        }

        #endregion // Public_Methods

        #region Private_Methods

        private static IEnumerable<string> GetRegexMatches(string source, string pattern)
        {
            foreach (Match m in Regex.Matches(source, pattern))
                yield return m.Groups[1].Value;
        }

        #endregion // Private_Methods
    }
}
