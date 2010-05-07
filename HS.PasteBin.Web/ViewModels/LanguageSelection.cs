using System.Collections.Generic;
using HS.SyntaxHighlighting;

namespace HS.PasteBin.Web.ViewModels
{
    public class LanguageSelection
    {
        public string SelectedLanguageKey { get; set; }
        public IEnumerable<Language> DefaultLanguages { get; set; }
        public IDictionary<string, string> OtherLanguages { get; set; }
    }
}
