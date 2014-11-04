using NUnit.Framework;
using _01_mark;

namespace Markdown_Tests
{
    [TestFixture]
    public class Markdown_processor_should
    {
        private MarkdownProcessor processor;

        [SetUp]
        public void init()
        {
            processor = new MarkdownProcessor();
        }

        [Test]
        public void start_paragraph_after_double_newlines()
        {
            var textWithDoubleNewlines = "\n    \nThis is new paragraph!";

            var processedText = processor.ReplaceMarkdownWithHtml(textWithDoubleNewlines);

            Assert.That(processedText, Is.EqualTo("<p>This is new paragraph!</p>"));
        }

        [Test]
        public void replace_underscore_with_em_tag()
        {
            var textWithUnderScore = "Текст с _землёй-земелькой_ пример";

            var processedText = processor.ReplaceMarkdownWithHtml(textWithUnderScore);
            var withEmTag = "<p>Текст с <em>землёй-земелькой</em> пример</p>";

            Assert.That(processedText, Is.EqualTo(withEmTag));
        }

        [Test]
        public void avoid_underscore_with_escape_char()
        {
            var textWithEscapeUnderscore = 
                @"\_Вот это\_, не должно выделиться тегом ";

            var processedText = processor.ReplaceMarkdownWithHtml(textWithEscapeUnderscore);
            
            Assert.That(processedText, Is.StringContaining("_Вот это_"));
        }

        [Test]
        public void replace_double_underscore_with_strong_tag()
        {
            var textWithUnderScore = 
                "__Двумя символами__ — должен становиться жирным с помощью тега <strong>.";

            var processedText = processor.ReplaceMarkdownWithHtml(textWithUnderScore);

            Assert.That(processedText, Is.StringContaining("<strong>Двумя символами</strong>"));
        }
    }
}
