using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;

namespace _01_mark
{
    public class MarkdownProcessor : IMarkdownEscapesProcessor
    {
        private List<Mark> marks;

        private static Dictionary<string, string> htmlRepresentation = new Dictionary<string, string>
        {
            {"<", "&lt;"},
            {">", "&gt;"},
            {"/", "&#47;"},
        };

        public MarkdownProcessor(Mark mark)
        {
            marks = GetMarks()
                .Where(m => m.GetType() != mark.GetType())
                .ToList();
        }

        public MarkdownProcessor()
        {
            marks = GetMarks();
        }

        private List<Mark> GetMarks()
        {
            return new List<Mark>
            {
                new ParagraphMark(this),
                new BacktickMark(this),
                new DoubleUnderscoreMark(this), 
                new SingleUnderscoreMark(this)
            };
        }

        public string ReplaceMarkdownWithHtml(string text)
        {
            text = Regex.Replace(text, @"[<>/]", ReplaceSpecialHtmlCharsToCodes);
            return marks.Aggregate(text, (current, mark) => mark.ProcessText(current));
        }

        private string ReplaceSpecialHtmlCharsToCodes(Match match)
        {
            return htmlRepresentation[match.Value];
        }

        public string AddEscapesToMarkdown(string text, IEnumerable<Type> ignoreMarks)
        {
            ignoreMarks = ignoreMarks ?? Enumerable.Empty<Type>();
            return marks
                .Where(mark => !ignoreMarks.Contains(mark.GetType()))
                .Aggregate(text, (current, mark) => mark.RoundMarksByEscapes(current));
        }
    }

    public interface IMarkdownEscapesProcessor
    {
        string AddEscapesToMarkdown(string text, IEnumerable<Type> ignoreMarks = null);
    }
}
