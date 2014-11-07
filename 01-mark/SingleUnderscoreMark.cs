namespace _01_mark
{
    class SingleUnderscoreMark : Mark
    {
        public SingleUnderscoreMark(IMarkdownEscapesProcessor escapesProcessor) : base(escapesProcessor)
        {
        }

        protected override string GetRegex()
        {
            return @"(?<=\s|\A|>)+\\{0,1}_{1}[^_]+_(?!_)";
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
