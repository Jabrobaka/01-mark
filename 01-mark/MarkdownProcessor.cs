using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_mark
{
    public class MarkdownProcessor
    {
        private List<Mark> marks;

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
                new ParagraphMark(),
                new BacktickMark(),
                new DoubleUnderscoreMark(), 
                new SingleUnderscoreMark()
            };
        }

        public string ReplaceMarkdownWithHtml(string text)
        {
            return marks.Aggregate(text, (current, mark) => mark.ProcessText(current));
        }

        public string AddEscapesToMarkdown(string text)
        {
            return marks.Aggregate(text, (current, mark) => mark.RoundMarksByEscapes(current));
        }
    }
}
