using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace _01_mark
{
    public class MarkdownProcessor 
    {
        private List<Mark> marks;
        private const char escape = '\\';

        public MarkdownProcessor()
        {
            marks = new List<Mark>
            {
                new BacktickMark(),
                new DoubleUnderscoreMark(), 
                new SingleUnderscoreMark()
            };
        }

        public string ReplaceMarkdownWithHtml(string text)
        {
            text = WebUtility.HtmlEncode(text);

            var processedText = ProcessParagraphs(text)
                .Select(p => marks.Aggregate(p, ProcessText));

            return String.Join(string.Empty, processedText);
        }

        private IEnumerable<string> ProcessParagraphs(string text)
        {
            const string regexPattern = @"(\r{0,1}\n\s*\r{0,1}\n)";

            return Regex.Split(text, regexPattern)
                .Select(s => Regex.Replace(s, regexPattern, ""))
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(s => string.Format("<p>{0}</p>", s));
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

            var removedMark = RemoveMark(stringWithMark, mark.Tag);

            if (mark.IgnoreMarkdownInsideTag)
            {
                removedMark = AddEscapesToMarkdown(removedMark);
            }

            return string.Format(mark.TagPattern, removedMark);
        }

        private string RemoveMark(string stringWithMark, string tag)
        {
            var withoutMark = stringWithMark
                .Substring(tag.Length, stringWithMark.Length - tag.Length * 2);
         
            return withoutMark;
        }

        private string AddEscapesToMarkdown(string text)
        {
            return marks
                .Aggregate(text, RoundMarkByEscapes);
        }

        private string RoundMarkByEscapes(string text, Mark mark)
        {
            return Regex.Replace(text, mark.Regex, match => RoundByEscapes(match, mark.Tag));
        }

        private string RoundByEscapes(Match match, string tag)
        {
            var stringToReplace = RemoveMark(match.Value, tag);
            var escapeFormat = "\\" + tag;
            return escapeFormat + stringToReplace + escapeFormat;
        }       
    }



}
