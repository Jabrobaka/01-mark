using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace _01_mark
{
    public class MarkdownProcessor
    {
        public MarkdownProcessor()
        {
            
        }

        public string ReplaceMarkdownWithHtml(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;    
          
            var textWithParagraphs = SetParagraphs(text);
            var withProcessedUnderscores = ProcessUnderscores(textWithParagraphs);
            return withProcessedUnderscores;
        }

        private string SetParagraphs(string text)
        {
            var paragraphs = Regex.Split(text, @"\n\s*\n")
                .Where(s => !string.IsNullOrEmpty(s))
                .Select(s => string.Format("<p>{0}</p>", s));
            return string.Join(string.Empty, paragraphs);
        }

        private string ProcessUnderscores(string text)
        {
            var replacedStrings = Regex.Replace(text, @"._(.+)_", ReplaceUnderscoreWithTag);
            return string.Join(string.Empty, replacedStrings);
        }

        private string ReplaceUnderscoreWithTag(Match match)
        {
            var stringWithUnderScore = match.Value;
            var escapeUnderscore = @"\_";
            if (stringWithUnderScore.StartsWith(escapeUnderscore) && 
                stringWithUnderScore.EndsWith(escapeUnderscore))
            {
                return "_" + 
                     stringWithUnderScore
                    .Remove(0, 2)
                    .Remove(stringWithUnderScore.Length - 4, 2)
                    + "_";
            }
            var removedUnderScores = stringWithUnderScore.Substring(2, match.Length - 3);
            var replacedWithTags = string.Format(" <em>{0}</em>", removedUnderScores);
            return replacedWithTags;
        }
    }
}
