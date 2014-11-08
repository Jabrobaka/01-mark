using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;

namespace _01_mark
{
    public class MarkdownProcessor 
    {
        private List<Mark> marks;
        private const char escape = '\\';

        private static Dictionary<string, string> htmlRepresentation = new Dictionary<string, string>
        {
            {"<", "&lt;"},
            {">", "&gt;"},
            {"/", "&#47;"},
        };

        public MarkdownProcessor()
        {
            marks = GetMarks();
        }

        private static List<Mark> GetMarks()
        {
            return new List<Mark>
            {
                new BacktickMark(),
                new DoubleUnderscoreMark(), 
                new SingleUnderscoreMark()
            };
        }

        public string ReplaceMarkdownWithHtml(string text)
        {
            text = Regex.Replace(text, @"[<>/]", ReplaceSpecialHtmlCharsToCodes);
            text = ProcessParagraphs(text);
            return marks.Aggregate(text, ProcessText);
        }

        private string ReplaceSpecialHtmlCharsToCodes(Match match)
        {
            return htmlRepresentation[match.Value];
        }

        private string ProcessParagraphs(string text)
        {
            const string regexPattern = @"(\r{0,1}\n\s*\r{0,1}\n)";
            var paragraphs = Regex.Split(text, regexPattern)
                .Select(s => Regex.Replace(s, regexPattern, ""))
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(s => string.Format("<p>{0}</p>", s));
            return string.Join(string.Empty, paragraphs);
        }

        private string ProcessText(string text, Mark mark)
        {
            return Regex.Replace(text, mark.Regex, match => ReplaceMarkWithTag(match, mark), RegexOptions.Singleline);
        }

        private string ReplaceMarkWithTag(Match match, Mark mark)
        {
            var stringWithMark = match.Value;

            if (stringWithMark.IsRoundedByEscapes(mark.Tag))
                return stringWithMark.ReplaceEscapeToNormal(mark.Tag);

            var removedMark = stringWithMark.RemoveMark(mark);

            if (mark.IgnoreMarkdownInsideTag)
            {
                removedMark = AddEscapesToMarkdown(removedMark);
            }

            return string.Format(mark.TagPattern, removedMark);
        }

        private string RemoveMark(string stringWithMark, Mark mark)
        {
            var withoutMark = stringWithMark
                .Substring(mark.Tag.Length, stringWithMark.Length - mark.Tag.Length * 2);

            if (mark.IgnoreMarkdownInsideTag)
            {
                return AddEscapesToMarkdown(withoutMark);
            }

            return withoutMark;
        }

        private string AddEscapesToMarkdown(string text)
        {
            return marks
                .Aggregate(text, RoundMarkByEscapes);
        }

        private string RoundMarkByEscapes(string text, Mark mark)
        {
            return Regex.Replace(text, mark.Regex, match => RoundByEscapes(match, mark));
        }

        private string RoundByEscapes(Match match, Mark mark)
        {
            var stringToReplace = RemoveMark(match.Value, mark);
            var escapeFormat = "\\" + mark.Tag;
            return escapeFormat + stringToReplace + escapeFormat;
        }       
    }

    public static class StringExtensions
    {
        private const char escape = '\\';

        public static bool IsRoundedByEscapes(this string stringToCheck, string tag)
        {
            return stringToCheck[0] == escape &&
                   stringToCheck[stringToCheck.Length - tag.Length - 1] == escape;
        }

        public static string ReplaceEscapeToNormal(this string stringWithEscape, string tag)
        {
            var withoutEscape = stringWithEscape
                .Substring(tag.Length + 1, stringWithEscape.Length - tag.Length * 2 - 2);

            return tag + withoutEscape + tag;
        }

        public static string RemoveMark(this string stringWithMark, Mark mark)
        {
            return stringWithMark
                .Substring(mark.Tag.Length, stringWithMark.Length - mark.Tag.Length * 2);
        }
    }

}
