using System.Collections.Generic;

namespace HS.SyntaxHighlighting
{
    public interface IHighlighter
    {
        IDictionary<string, string> Languages { get; }
        string Highlight(string text, string languageKey);
    }
}
