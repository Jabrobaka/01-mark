namespace _01_mark
{
    class DoubleUnderscoreMark : Mark
    {
        public override string Regex
        {
            get { return @"(?<!_|\w)+\\{0,1}_{2}[^_]+_{2}(?!\w)"; }
        }

        public override string Tag
        {
            get { return "__"; }
        }

        public override string TagPattern
        {
            get { return "<strong>{0}</strong>"; }
        }
    }
}
