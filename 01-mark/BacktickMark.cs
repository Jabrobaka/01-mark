using System.Text.RegularExpressions;

namespace _01_mark
{
    class BacktickMark : Mark
    {
        public override bool IgnoreMarkdownInsideTag
        {
            get { return true; }
        }

        public override string Regex
        {
            get { return @"(?<=.*)\\{0,1}`{1}[^`]*`{1}(?=.*)"; }
        }

        public override string Tag
        {
            get { return "`"; }
        }

        public override string TagPattern
        {
            get { return "<code>{0}</code>"; }
        }
    }
}
