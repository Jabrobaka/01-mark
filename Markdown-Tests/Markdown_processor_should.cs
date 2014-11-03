using NUnit.Framework;
using _01_mark;

namespace Markdown_Tests
{
    [TestFixture]
    public class Markdown_processor_should
    {
        [Test]
        public void start_paragraph_after_double_newlines()
        {
            var processor = new MarkdownProcessor();
            var textWithDoubleNewlines = "\n    \nThis is new paragraph!";

            var processedText = processor.ReplaceMarkdownWithHtml(textWithDoubleNewlines);

            Assert.That(processedText, Is.EqualTo("<p>This is new paragraph!</p>"));
        }

        [Test]
        public void replace_underscore_with_em_tag()
        {
            var processor = new MarkdownProcessor();
            var textWithUnderScore = "Текст с _землёй-земелькой_ пример";

            var processedText = processor.ReplaceMarkdownWithHtml(textWithUnderScore);
            var withEmTag = "Текст с <em>землёй-земелькой</em> пример";

            Assert.That(processedText, Is.EqualTo(withEmTag));
        }
    }
}
