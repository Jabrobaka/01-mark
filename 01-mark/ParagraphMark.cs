using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _01_mark
{
    class ParagraphMark : IMark
    {
        public string ProcessText(string text)
        {
            var paragraphs = Regex.Split(text, @"\n\s*\n")
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(s => string.Format("<p>{0}</p>", s));
            return string.Join(string.Empty, paragraphs);
        }
    }
}
