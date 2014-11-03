using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace _01_mark
{
    public class MarkdownProcessor
    {

        public string ReplaceMarkdownWithHtml(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;    
          
            var textWithParagraphs = SetParagraphs(text);
            var withProcessedUnderscores = ProcessUnderscores(textWithParagraphs);
            return withProcessedUnderscores;
        }

        private string SetParagraphs(string text)
        {
            var paragraphs = Regex.Split(text, @"\n\s*\n")
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(s => string.Format("<p>{0}</p>", s));
            return string.Join(string.Empty, paragraphs);
        }

        private string ProcessUnderscores(string text)
        {
            var replacedStrings = Regex.Replace(text, @"\\*_(.+)\\*_", ReplaceUnderscoreWithTag);
            return string.Join(string.Empty, replacedStrings);
        }

        private string ReplaceUnderscoreWithTag(Match match)
        {
            var stringWithUnderscore = match.Value;

            if (stringWithUnderscore.IsRoundedByEscapes())
                return stringWithUnderscore.ReplaceEscapeToNormal();

            var removedUnderscores = stringWithUnderscore.RemoveMarkdownSymbols();
            string replacedWithTags;
            return ReplaceWithTag(stringWithUnderscore, removedUnderscores);
        }

        private static string ReplaceWithTag(string stringWithUnderscore, string removedUnderscores)
        {
            string replacedWithTags;
            if (stringWithUnderscore.IsStrongType())
                replacedWithTags = string.Format("<strong>{0}</strong>",
                    removedUnderscores.RemoveMarkdownSymbols()); //еще разок
            else
                replacedWithTags = string.Format("<em>{0}</em>", removedUnderscores);

            return replacedWithTags;
        }
    }

    static class StringExtensions
    {

        public static bool IsRoundedByEscapes(this string stringToCheck)
        {
            var escapeStart = '\\';
            return stringToCheck[0] == escapeStart && 
                stringToCheck[stringToCheck.Length - 2] == escapeStart;
        } 

        public static string RemoveMarkdownSymbols(this string stringWithSymbol)
        {
            return stringWithSymbol.Substring(1, stringWithSymbol.Length - 2);
        }

        public static string ReplaceEscapeToNormal(this string stringWithEscape)
        {
            var length = stringWithEscape.Length;
            return stringWithEscape.Substring(1, length - 3) +
                   stringWithEscape[length - 1];
        }

        public static bool IsStrongType(this string stringToCheck)
        {
            var strongUnderscore = "__";
            return stringToCheck.StartsWith(strongUnderscore) &&
                stringToCheck.EndsWith(strongUnderscore);
        }
    }
}
