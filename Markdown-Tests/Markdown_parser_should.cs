using NUnit.Framework;
using _01_mark;

namespace Markdown_Tests
{
    [TestFixture]
    public class Markdown_parser_should
    {
        [Test]
        public void start_paragraph_after_double_newlines()
        {
            var parser = new MarkdownParser();
            var textWithDoubleNewlines = "\n    \n This is new paragraph!";

            var outHtml = parser.Parse(textWithDoubleNewlines);

            Assert.That(outHtml, Is.EqualTo("<p>This is new paragraph!</p>"));
        }

        public static void Main(string[] args)
        {
            
        }
    }
}
