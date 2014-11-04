using System;
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

        [TestCase("_")]
        [TestCase("__")]
        public void avoid_processing_mark_with_escape_char(String tag)
        {
            var textWithEscapeUnderscore = string.Format(
                @"\{0}Вот это\{0}, не должно выделиться тегом ", tag);

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

        [TestCase("Внутри _выделения <em> может быть __<strong>__ выделение_",
            "<em>выделения <em> может быть <strong><strong></strong> выделение</em>")]
        public void process_marks_inside_another_marks(string input, string expected)
        {
            var processedText = processor.ReplaceMarkdownWithHtml(input);

            Assert.That(processedText, Is.StringContaining(expected));
        }

        [Test]
        public void replace_backtick_with_code_tag()
        {
            var textWithBackTicks =
                "Текст с `кодом` йоу";

            var processedText = processor.ReplaceMarkdownWithHtml(textWithBackTicks);

            Assert.That(processedText, Is.StringContaining("с <code>кодом</code> "));
        }

        [Test]
        public void ignore_markdown_inside_backticks()
        {
            var textWithUnderscoreInsideBacktick =
                "Текст, в `котором _не должно_ появиться` тега <em>";

            var processedText = processor.ReplaceMarkdownWithHtml(textWithUnderscoreInsideBacktick);

            Assert.That(processedText, Is.StringContaining("<code>котором _не должно_ появиться</code<"));
        }
    }
}
