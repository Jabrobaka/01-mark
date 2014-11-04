using System.Text.RegularExpressions;

namespace _01_mark
{
    class BacktickMark : Mark
    {
        public BacktickMark()
        {
            ingoreMarkdownInsideMark = true;
        }

        protected override string GetRegex()
        {
            return @"(?<=.*)`{1}[^`]*`{1}(?=.*)";
        }

        protected override string GetTag()
        {
            return "`";
        }

        protected override string GetTagPattern()
        {
            return "<code>{0}</code>";
        }
    }
}
