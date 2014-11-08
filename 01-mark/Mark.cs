using System.Text.RegularExpressions;

namespace _01_mark
{
    public abstract class Mark
    {
        public virtual bool IgnoreMarkdownInsideTag { get { return false; } }
        public abstract string Regex { get; }
        public abstract string Tag { get; }
        public abstract string TagPattern { get; }
        
    }
}
