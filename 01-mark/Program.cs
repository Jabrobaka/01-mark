using System.IO;

namespace _01_mark
{
    class Program
    {
        private static string htmlFilePattern =
            "<!DOCTYPE html> <html> <head> <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /></head><body>{0}</body></html>";

        static void Main(string[] args)
        {
            var filename = args[0];
            var processedText = new MarkdownProcessor()
                .ReplaceMarkdownWithHtml(File.ReadAllText(filename));
            var htmlFileText = string.Format(htmlFilePattern, processedText);
            File.WriteAllText(Path.ChangeExtension(filename, ".html"), htmlFileText);
        }
    }
}
