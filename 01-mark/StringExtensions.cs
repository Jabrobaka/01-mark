namespace _01_mark
{
    public static class StringExtensions
    {
        private const char escape = '\\';

        public static bool IsRoundedByEscapes(this string stringToCheck, string tag)
        {
            return stringToCheck[0] == escape &&
                   stringToCheck[stringToCheck.Length - tag.Length - 1] == escape;
        }

        public static string ReplaceEscapeToNormal(this string stringWithEscape, string tag)
        {
            var withoutEscape = stringWithEscape
                .Substring(tag.Length + 1, stringWithEscape.Length - tag.Length * 2 - 2);

            return tag + withoutEscape + tag;
        }
    }
}
