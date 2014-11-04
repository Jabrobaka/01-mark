namespace _01_mark
{
    class SingleUnderscoreMark : UnderscoreMark
    {
        protected override string GetRegex()
        {
            return @"(?<=[^_])\\{0,1}_{1}[^_](.*)\\{0,1}_{1}";
        }

        protected override string GetTag()
        {
            return "_";
        }

        protected override string GetTagPattern()
        {
            return "<em>{0}</em>";
        }
    }
}
