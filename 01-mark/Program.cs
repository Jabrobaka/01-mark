using System;
using System.IO;
using System.Text;

namespace _01_mark
{
    class Program
    {  
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Ожидался ввод имени файла в качестве аргумента при запуске\nПопробуйте снова");
                Console.ReadKey();
                return;
            }

            var filename = args[0];
            var fileText = File.ReadAllText(filename);
            var processedText = new MarkdownProcessor()
                .ReplaceMarkdownWithHtml(fileText);
            var htmlFileText = GetHtmlFormatText(processedText);
            File.WriteAllText(Path.ChangeExtension(filename, ".html"), htmlFileText);
        }

        private static string GetHtmlFormatText(string processedText)
        {
            var htmlFilePattern = new StringBuilder()
                .AppendLine("<!DOCTYPE html>")
                .AppendLine("<html>")
                .AppendLine("<head>")
                .AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />")
                .AppendLine("</head>")
                .AppendLine("<body>")
                .AppendLine("{0}")
                .AppendLine("</body>")
                .ToString();
            return string.Format(htmlFilePattern, processedText);
        }
    }
}
