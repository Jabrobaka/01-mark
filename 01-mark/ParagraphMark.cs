using System.Linq;
using System.Text.RegularExpressions;

namespace _01_mark
{
    class ParagraphMark : Mark
    {
        protected override string GetRegex()
        {
//            return @"\n\s*\n";
//            return @"(\n\s*\n).*?(?=\Z|\1)";
//            return @"(\r{0,1}\n\s*\r{0,1}\n).*?(?=\Z|\1)";
            return @"(\r{0,1}\n\s*\r{0,1}\n)";
        }

        protected override string GetTag()
        {
            return string.Empty;
        }

        protected override string GetTagPattern()
        {
            return "<p>{0}</p>";
        }

        protected override string RemoveMark(string stringWithMark)
        {
//            return Regex.Replace(stringWithMark, @"(\n\s*\n)", "");
            return Regex.Replace(stringWithMark, @"(\r{0,1}\n\s*\r{0,1}\n)", "");
        }

        public override string ProcessText(string text)
        {
            var paragraphs = Regex.Split(text, regexPattern)
                .Select(RemoveMark)
                .Where(s => !string.IsNullOrEmpty(s))              
                .Select(s => string.Format(tagPattern, s));
            return string.Join(string.Empty, paragraphs);
        }

        
    }
}
