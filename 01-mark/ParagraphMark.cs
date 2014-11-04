using System.Linq;
using System.Text.RegularExpressions;

namespace _01_mark
{
    class ParagraphMark : Mark
    {
        protected override string GetRegex()
        {
            return @"\n\s*\n";
        }

        protected override string GetTag()
        {
            return string.Empty;
        }

        protected override string GetTagPattern()
        {
            return "<p>{0}</p>";
        }

        public override string ProcessText(string text)
        {
            var paragraphs = Regex.Split(text, regexPattern)
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(s => string.Format(tagPattern, s));
            return string.Join(string.Empty, paragraphs);
        }

        
    }
}
