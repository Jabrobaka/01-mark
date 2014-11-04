namespace _01_mark
{
    class SingleUnderscoreMark : Mark
    {
        protected override string GetRegex()
        {
            return @"(?<=[\s>.,])\\{0,1}_{1}[^_](.*)\\{0,1}_{1}(?=[\s<.,])";
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
