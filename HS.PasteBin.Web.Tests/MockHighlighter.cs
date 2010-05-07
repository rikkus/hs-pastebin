using System.Collections.Generic;
using HS.SyntaxHighlighting;

namespace HS.PasteBin.Web.Tests
{
    public class MockHighlighter : IHighlighter
    {
        private readonly IDictionary<string, string> languages;

        public MockHighlighter(IDictionary<string, string> languages)
        {
            this.languages = languages;
        }

        public IDictionary<string, string> Languages
        {
            get { return languages; }
        }

        public string Highlight(string text, string languageKey)
        {
            return languages[languageKey] + "|" + text;
        }
    }
}
