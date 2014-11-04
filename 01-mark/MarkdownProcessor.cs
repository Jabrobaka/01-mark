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
        private List<IMark> marks;

        public MarkdownProcessor()
        {
            marks = new List<IMark>();
            marks.Add(new ParagraphMark());
            marks.Add(new DoubleUnderscoreMark());
            marks.Add(new SingleUnderscoreMark());
        }

        public string ReplaceMarkdownWithHtml(string text)
        {
            return marks.Aggregate(text, (current, mark) => mark.ProcessText(current));
        }
    }
}
