namespace _01_mark
{
    class DoubleUnderscoreMark : Mark
    {
        protected override string GetRegex()
        {
            return @"(?<!_|\w)+\\{0,1}_{2}[^_]+_{2}(?!\w)";
        }

        protected override string GetTag()
        {
            return "__"; 
        }

        protected override string GetTagPattern()
        {
            return "<strong>{0}</strong>";
        }
    }
}
