using System.Linq;
using System.Text.RegularExpressions;

namespace _01_mark
{
    public class MarkdownProcessor
    {
        public MarkdownProcessor()
        {
            
        }

        public string ReplaceMarkdownWithHtml(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;              
            var textSplittedByParagraphs = GetParagraphs(text);
            return textSplittedByParagraphs;
        }

        private string GetParagraphs(string text)
        {
            var paragraphs = Regex.Split(text, @"\n\s*\n")
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(s => string.Format("<p>{0}</p>", s));
            return string.Join(string.Empty, paragraphs);
        }
    }
}
