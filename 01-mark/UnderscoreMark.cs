using System.Text.RegularExpressions;

namespace _01_mark
{
    abstract class UnderscoreMark : IMark
    {
        private string underscoreRegex;
        private string tag;
        private string tagPattern;
        private char escape = '\\';

        protected UnderscoreMark()
        {
            tagPattern = GetTagPattern();
            tag = GetTag();
            underscoreRegex = GetRegex();
        }

        protected abstract string GetRegex();

        protected abstract string GetTag();

        protected abstract string GetTagPattern();

        public string ProcessText(string text)
        {
            return Regex.Replace(text, underscoreRegex, ReplaceUnderscoreWithTag);
        }
            
        private string ReplaceUnderscoreWithTag(Match match)
        {
            var stringWithUnderscore = match.Value;

            if (IsRoundedByEscapes(stringWithUnderscore))
                return ReplaceEscapeToNormal(stringWithUnderscore);

            var removedUnderscores = RemoveUnderscores(stringWithUnderscore);
            return string.Format(tagPattern, removedUnderscores);
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

        private string RemoveUnderscores(string stringWithUnderscores)
        {
            return stringWithUnderscores
                .Substring(tag.Length, stringWithUnderscores.Length - tag.Length * 2);
        }       
    }
}
