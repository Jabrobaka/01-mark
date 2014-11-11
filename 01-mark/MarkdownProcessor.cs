using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace _01_mark
{
    public class MarkdownProcessor 
    {
        private List<Mark> marks;
        private const char escape = '\\';
        const string paragraphRegex = @"(\r{0,1}\n\s*\r{0,1}\n)";

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

            var processedMarks = marks
                .Aggregate(text, ProcessText);

            var withParagraphs = ProcessParagraphs(processedMarks);

            return String.Join(string.Empty, withParagraphs);
        }

        private IEnumerable<string> ProcessParagraphs(string text)
        {
           return Regex.Split(text, paragraphRegex)
                .Select(s => Regex.Replace(s, paragraphRegex, ""))
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

            var withRemovedMark = RemoveMark(stringWithMark, mark.Tag);

            if (mark.IgnoreMarkdownInsideTag)
            {
                withRemovedMark = AddEscapesToMarkdown(withRemovedMark);
            }

            var specialProcessed = mark.SpecialProcessing(withRemovedMark);

            return string.Format(mark.TagPattern, specialProcessed);
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
