namespace _01_mark
{
    class SingleUnderscoreMark : Mark
    {
        public override bool IgnoreMarkdownInsideTag
        {
            get { return false; }
        }

        public override string Regex
        {
            get { return @"(?<=\s|\A|>)+\\{0,1}_{1}[^_]+_(?!_)"; }
        }

        public override string Tag
        {
            get { return "_"; }
        }

        public override string TagPattern
        {
            get { return "<em>{0}</em>"; }
        }
    }
}
