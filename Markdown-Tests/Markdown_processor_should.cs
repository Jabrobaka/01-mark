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

        [TestCase("\n\n")]
        [TestCase("\n    \n")]
        [TestCase("\r\n\r\n")]
        [TestCase("\r\n   \r\n")]
        public void start_paragraph_after_double_newlines(string newlines)
        {
            var textWithDoubleNewlines = string.Format("{0}This is new paragraph!", newlines);

            var processedText = processor.ReplaceMarkdownWithHtml(textWithDoubleNewlines);

            Assert.That(processedText, Is.EqualTo("<p>This is new paragraph!</p>"));
        }

        [Test]
        public void make_two_ps_in_text_with_two_paragraphs()
        {
            var text = "Параграф\r\n\r\nПараграф\r\n \r\nПараграф";

            var processedText = processor.ReplaceMarkdownWithHtml(text);

            Assert.That(processedText, Is.EqualTo("<p>Параграф</p><p>Параграф</p><p>Параграф</p>"));
        }

        [Test]
        public void round_with_p_tag_text_without_double_newlines()
        {
            var text = "Этот текст должен быть в теге";

            var processed = processor.ReplaceMarkdownWithHtml(text);

            Assert.That(processed, Is.EqualTo("<p>Этот текст должен быть в теге</p>"));
        }

        [Test]
        public void replace_underscore_with_em_tag()
        {
            var textWithUnderScore = "Текст _окруженный с двух сторон_ йоу";

            var processedText = processor.ReplaceMarkdownWithHtml(textWithUnderScore);

            Assert.That(processedText, Is.StringContaining("<em>окруженный с двух сторон</em>"));
        }

        [TestCase("_")]
        [TestCase("__")]
        [TestCase("`")]
        public void avoid_processing_mark_with_escape_char(String tag)
        {
            var textWithEscapeUnderscore = string.Format(
                @"\{0}Вот это\{0}, не должно выделиться тегом ", tag);

            var processedText = processor.ReplaceMarkdownWithHtml(textWithEscapeUnderscore);
            
            Assert.That(processedText, Is.StringContaining(tag + "Вот это" + tag));
        }

        [Test]
        public void replace_double_underscore_with_strong_tag()
        {
            var textWithUnderScore = 
                "__Двумя символами__ — должен становиться жирным с помощью тега <strong>.";

            var processedText = processor.ReplaceMarkdownWithHtml(textWithUnderScore);

            Assert.That(processedText, Is.StringContaining("<strong>Двумя символами</strong>"));
        }

        [Test]
        public void process_marks_inside_another_marks()
        {
            var text = "Внутри _выделения <em> может быть __<strong>__ выделение_";
            var expected = 
                "<em>выделения &lt;em&gt; может быть <strong>&lt;strong&gt;</strong> выделение</em>";
            
            var processedText = processor.ReplaceMarkdownWithHtml(text);

            Assert.That(processedText, Is.StringContaining(expected));
        }

        [Test]
        public void ignore_underscores_inside_text_without_whitespaces()
        {
            var text = "Подчерки_внутри_текста__и__цифр_12_3 не считаются выделением";

            var processed = processor.ReplaceMarkdownWithHtml(text);

            Assert.That(processed, Is.Not.StringContaining("em"));
            Assert.That(processed, Is.Not.StringContaining("strong"));
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

            Assert.That(processedText, Is.StringContaining("<code>котором _не должно_ появиться</code>"));
        }

        [Test]
        public void ignore_paragraphs_inside_code()
        {
            var text = "Текст `с кодом без \n\nлишних` тегов";

            var processed = processor.ReplaceMarkdownWithHtml(text);

            Assert.That(processed, Is.Not.Contains("<p>лишних</p>"));
        }

        [Test]
        public void ignore_unpaired_marks()
        {
            var text = "_тут__есть_всякие `непарные символы\nйоу";

            var processed = processor.ReplaceMarkdownWithHtml(text);

            Assert.That(processed, Is.StringContaining("_тут__есть_всякие `непарные символы\nйоу"));
        }
    }
}
