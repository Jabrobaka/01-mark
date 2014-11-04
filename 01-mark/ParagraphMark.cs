using System.Text.RegularExpressions;

namespace _01_mark
{
    class ParagraphMark : Mark
    {
        protected override string GetRegex()
        {
//            return @"\n\s*\n";
            return @"(\n\s*\n).*?(?=\Z|\1)";
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
            return Regex.Replace(stringWithMark, @"(\n\s*\n)", "");
        }

//        public override string ProcessText(string text)
//        {
//            var paragraphs = Regex.Split(text, regexPattern)
//                .Where(s => !string.IsNullOrEmpty(s))
//                .Select(s => string.Format(tagPattern, s));
//            return string.Join(string.Empty, paragraphs);
//        }

        
    }
}
