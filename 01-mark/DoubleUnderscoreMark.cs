namespace _01_mark
{
    class DoubleUnderscoreMark : Mark
    {
        protected override string GetRegex()
        {
            return @"(?<=\W)\\{0,1}__[^_](.*)\\{0,1}__(?=\W)";
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
