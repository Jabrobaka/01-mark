namespace _01_mark
{
    class DoubleUnderscoreMark : Mark
    {
        protected override string GetRegex()
        {
            return @"\\{0,1}__[^_](.*)\\{0,1}__";
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
