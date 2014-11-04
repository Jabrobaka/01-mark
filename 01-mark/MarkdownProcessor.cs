using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _01_mark
{
    public class MarkdownProcessor
    {
        private List<Mark> marks;

        private static Dictionary<string, string> htmlRepresentation = new Dictionary<string, string>
        {
            {"<", "&lt;"},
            {">", "&gt;"},
            {"/", "&quot;"},
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

        private static List<Mark> GetMarks()
        {
            return new List<Mark>
            {
                new ParagraphMark(),
                new BacktickMark(),
                new DoubleUnderscoreMark(), 
                new SingleUnderscoreMark()
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

        public string AddEscapesToMarkdown(string text)
        {
            return marks.Aggregate(text, (current, mark) => mark.RoundMarksByEscapes(current));
        }
    }
}
