using System.Text.RegularExpressions;

namespace _01_mark
{
    public abstract class Mark
    {
        protected string regexPattern;
        protected string tag;
        protected string tagPattern;
        protected char escape = '\\';
        protected bool ingoreMarkdownInsideMark = false;

        protected Mark()
        {
            tagPattern = GetTagPattern();
            tag = GetTag();
            regexPattern = GetRegex();
        }

        protected abstract string GetRegex();

        protected abstract string GetTag();

        protected abstract string GetTagPattern();

        public virtual string ProcessText(string text)
        {
            return Regex.Replace(text, regexPattern, ReplaceMarkWithTag, RegexOptions.Singleline);
        }

        public string RoundMarksByEscapes(string text)
        {
            return Regex.Replace(text, regexPattern, RoundByEscapes);
        }


        private string ReplaceMarkWithTag(Match match)
        {
            var stringWithMark = match.Value;

            if (IsRoundedByEscapes(stringWithMark))
                return ReplaceEscapeToNormal(stringWithMark);

            var removedMark = RemoveMark(stringWithMark);
            return string.Format(tagPattern, removedMark);
        }

        private bool IsRoundedByEscapes(string stringToCheck)
        {
            return stringToCheck[0] == escape &&
                   stringToCheck[stringToCheck.Length - tag.Length - 1] == escape;
        }

        private string ReplaceEscapeToNormal(string stringWithEscape)
        {
            var withoutEscape = stringWithEscape
                .Substring(tag.Length + 1, stringWithEscape.Length - tag.Length * 2 - 2);
            
            return tag + withoutEscape + tag;
        }

        private string RemoveMark(string stringWithMark)
        {
            if (ingoreMarkdownInsideMark)
            {
                stringWithMark = new MarkdownProcessor(this)
                    .AddEscapesToMarkdown(stringWithMark);
            }
            return stringWithMark
                .Substring(tag.Length, stringWithMark.Length - tag.Length * 2);
        }

        private string RoundByEscapes(Match match)
        {
            var stringToReplace = RemoveMark(match.Value);
            var escapeFormat = "\\" + tag;
            return escapeFormat + stringToReplace + escapeFormat;
        }
    }
}
